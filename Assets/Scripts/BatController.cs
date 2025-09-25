using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BatController : MonoBehaviour
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
    private int maxHealth = 30;
    private int currentHealth;
    public bool isDead = false;

    public static BatController instance;
    public Slider healthSlider;

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
            Vector3 targetPos = player.position + new Vector3(0, 1.5f, 0);
            Vector3 direction = (targetPos - transform.position).normalized;
            float wave = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
            Vector3 waveOffset = new Vector3(0, wave, 0);
            Vector3 moveDir = direction + waveOffset;



            transform.position += moveDir * moveSpeed * Time.deltaTime;

            Flip(player);
            Animator.SetBool("isAttack", true);

            if (distanceToPlayer <= detectionRadius)
            {
                if (shootTimer >= shootCooldown)
                {
                    Shoot(player);
                    shootTimer = 0f;
                }
            }
            
        }
        else
        {
            Animator.SetBool("isAttack", false);
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
        currentHealth -= damage;
        MusicManager.Instance.playSFX("bat_hurt");
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
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
        EnemyPool.Instance.ReturnBat(gameObject);

    }
    public void ResetEnemy()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            healthSlider.gameObject.SetActive(true);
        }
        if (Animator == null)
            Animator = GetComponent<Animator>();

        Animator.SetBool("isAttack", false);
    }
    private IEnumerator Flash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }

    void Shoot(Transform target)
    {
        GameObject Batbullet = BulletPool.Instance.GetBatBullet();
        Batbullet.transform.position = firePoint.position;

        Vector3 direction = (target.position - firePoint.position).normalized;
        Batbullet.GetComponent<BatBulletScript>().SetDirection(direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
