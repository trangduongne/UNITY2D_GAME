using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DinoScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Transform player;
    private float patrolSpeed = 2f;
    private float chaseSpeed = 3.5f;
    private float detectRange = 3f;
    private float chaseDistance = 5f;
    public float waitTime = 2f;
    public float lostWaitTime = 1.5f; 
    private bool isReturning = false;
    public GameObject exclamation_point;
    public GameObject question;

    private Transform targetPoint;
    private bool isWaiting = false;
    private bool isChasing = false;
    private Vector3 chaseStartPos;
    private SpriteRenderer spriteRenderer;
    public Animator animator;

    public float maxHealth = 10;
    public float currentHealth;
    public static DinoScript instance;
    public bool isDead = false;

    public Slider healthSlider;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        targetPoint = pointB;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (isWaiting || isReturning) return;


        question.SetActive(false);
        if (!isChasing && Vector2.Distance(transform.position, player.position) < detectRange)
        {
            isChasing = true;
            chaseStartPos = transform.position;
        }

        if (isChasing)
            ChasePlayer();
        else
            Patrol();

        if (isChasing || !isWaiting && !isReturning)
        {
            Flip();
        }
        FixIconDirection();
    }
    void FixIconDirection()
    {
        if (exclamation_point != null)
            exclamation_point.transform.localScale = new Vector3(0.1f, 0.1f, 1);

        if (question != null)
            question.transform.localScale = new Vector3(0.05f, 0.05f, 1);
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);
        animator.SetBool("isRun", true);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    void ChasePlayer()
    {
        exclamation_point.SetActive(true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        animator.SetBool("isRun", true);

        float distanceFromStart = Vector2.Distance(chaseStartPos, transform.position);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceFromStart >= chaseDistance || distanceToPlayer > detectRange)
        {
            StartCoroutine(PauseBeforeReturn());
        }
    }
    IEnumerator PauseBeforeReturn()
    {
        exclamation_point.SetActive(false);
        question.SetActive(true);
        isChasing = false;
        isReturning = true;
        animator.SetBool("isRun", false);

        yield return new WaitForSeconds(lostWaitTime);

        targetPoint = (targetPoint == pointA) ? pointB : pointA;
        isReturning = false;
    }


    IEnumerator WaitAtPoint()
    {
        exclamation_point.SetActive(false);
        animator.SetBool("isRun", false);
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
        isWaiting = false;
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = (GetTarget().x < transform.position.x) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    Vector3 GetTarget()
    {
        return isChasing ? player.position : targetPoint.position;
    }
    public void TakeDamage(int damage)
    {
        MusicManager.Instance.playSFX("dino_hurt");
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
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
        ManagerScene1.Instance.AddKill();
        EnemyPool.Instance.ReturnDino(gameObject);

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
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

    }
    private IEnumerator Flash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }



}
