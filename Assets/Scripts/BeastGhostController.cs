using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BeastGhostController : MonoBehaviour
{
    public float detectionRadius = 3f;
    public float chaseRadius = 6f;
    public LayerMask Player;
    public Transform firePoint;
    public float shootCooldown = 2f;
    public float moveSpeed = 2f;
    public float waveFrequency = 2f;
    private float waveAmplitude = 1f;

    private float shootTimer;
    private Transform player;
    private Animator Animator;

    private SpriteRenderer spriteRenderer;
    private int maxHealth = 100;
    private int currentHealth;
    public bool isDead = false;

    public static BeastGhostController instance;
    public Slider healthSlider;
    private float baseHeightOffset = 2f;


    public GameObject warningZonePrefab;

    private bool isCastingUltimate = false;
    private bool isCastingUltimate2 = false;
    Vector3[] spawnOffsets = new Vector3[]
    {
    new Vector3(-2f, -0.5f, 0),
    new Vector3(0f, -0.5f, 0),
    new Vector3(2f, -0.5f, 0),
    new Vector3(4f, -0.5f, 0),
    new Vector3(6f, -0.5f, 0)
    };


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }


    void Update()
    {
        shootTimer += Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRadius)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y + baseHeightOffset, player.position.z);
            Vector3 direction = (targetPos - firePoint.position).normalized;
            float wave = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
            Vector3 waveOffset = new Vector3(0, wave, 0);
            Vector3 moveDir = direction + waveOffset;



            transform.position += moveDir * moveSpeed * Time.deltaTime;

            Flip(player);

            if (distanceToPlayer <= detectionRadius)
            {
                if (shootTimer >= shootCooldown)
                {
                    Shoot(player);
                    shootTimer = 0f;
                }
            }
            if (!isCastingUltimate)
            {
                StartCoroutine(UltimateSkill());
                isCastingUltimate = true;
            }
            if(healthSlider.value <50 && !isCastingUltimate2)
            {
                StartCoroutine(UltimateSkillFinal());
                isCastingUltimate2 = true;
            }    
        }
    }
    void CastGroundAttack()
    {
        Vector3 spawnPos = player.position + new Vector3(0, -0.5f, 0); 
        GameObject zone = Instantiate(warningZonePrefab, spawnPos, Quaternion.identity);
    }
    void CastGroundAttack2()
    {
        foreach (Vector3 offset in spawnOffsets)
        {
            Vector3 spawnPos = player.position + offset;
            Instantiate(warningZonePrefab, spawnPos, Quaternion.identity);
        }
    }
    IEnumerator UltimateSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Animator.SetTrigger("isCastSpell");
            CastGroundAttack();
        }
    }
    IEnumerator UltimateSkillFinal()
    {
        spriteRenderer.color = Color.magenta;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Animator.SetTrigger("isCastSpell");
            CastGroundAttack2();
        }
    }
    void Flip(Transform target)
    {
        Vector3 scale = transform.localScale;
        scale.x = (target.position.x < transform.position.x) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    public void TakeDamage(int damage)
    {
        MusicManager.Instance.playSFX("ghost_hurt");
        currentHealth -= damage;
        StartCoroutine(Flash());

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        Animator.SetBool("isDie", true);
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }        
        ManagerScene1.Instance.GameWin();

        EnemyPool.Instance.ReturnBeastGhost(gameObject);

    }
    public void ResetEnemy()
    {
        Animator.SetBool("isDie", false);
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            healthSlider.gameObject.SetActive(true);
        }
        if (Animator == null)
            Animator = GetComponent<Animator>();

    }
    private IEnumerator Flash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }

    void Shoot(Transform target)
    {
        GameObject BeastGhostBullet = BulletPool.Instance.GetBeastGhostBullet();
        BeastGhostBullet.transform.position = firePoint.position;

        Vector3 direction = (target.position - firePoint.position).normalized;
        BeastGhostBullet.GetComponent<BeastGhostBullet>().SetDirection(direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
