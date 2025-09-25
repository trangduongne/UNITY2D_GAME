using System.Collections;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 3f;
    public float JumpForce = 5f;
    public float RollForce = 5f;
    public float SlideForce = 2.0f;
    public Vector2 move;

    public bool isJump;
    public bool isCrouch;
    public bool isRoll;
    public bool isSlide;
    public bool isDead = false;
    public bool isAttack = false;

    public float life = 3;


    [Header("Unity Component")]
    Rigidbody2D rb;
    Animator animator;

    bool isGrounded;

    public Button Skill1;
    public Button Skill2;
    public Button SkillHeal;

    private SpriteRenderer spriteRenderer;
    public static PlayerController Instance;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private float damageCooldown = 0.2f;
    private float lastDamageTime = 0f;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Skill1.onClick.AddListener(() => PlayerSkills.instance.Skill1(updateAniSkill1));
        Skill2.onClick.AddListener(() => PlayerSkills.instance.Skill2(updateAniSkill2));
        SkillHeal.onClick.AddListener(() => PlayerSkills.instance.SkillHeal());
    }

    void Update()
    {
        if (isDead) return;
        if (ManagerScene1.Instance.isPaused) return;
        move = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (move.x == 0) animator.SetBool("isRun", false);
        else if (move.x != 0 && !isCrouch)
        {
            move = move.normalized * moveSpeed *Time.deltaTime;
            animator.SetBool("isRun", true);
            Flip();
            transform.Translate(move);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouch)
        {
            MusicManager.Instance.playSFX("jump");
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJump",true);
            isJump = true;
        }
        if(Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            animator.SetBool("isCrouch",true);
            isCrouch = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && isCrouch)
        {
            animator.SetBool("isCrouch", false);
            
            isCrouch = false;
        }
        if(Input.GetKeyDown(KeyCode.R) && isGrounded && !isCrouch && !isRoll)
        {
            StartCoroutine(Roll());
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isCrouch && !isSlide)
        {
            StartCoroutine(Slide());
        }    
        if(Input.GetKeyDown(KeyCode.C) && !isCrouch && !isRoll)
        {
            MusicManager.Instance.playSFX("sword");
            animator.SetTrigger("isAttack1");
            PerformAttack(1);
            StartCoroutine(SetAttackState(0.3f));
        }
        if(Input.GetKeyDown(KeyCode.V) && isGrounded && !isCrouch && !isRoll)
        {
            MusicManager.Instance.playSFX("sword");
            animator.SetTrigger("isAttack2");
            PerformAttack(2);
            StartCoroutine(SetAttackState(0.3f));
        }
        if(Input.GetKeyDown(KeyCode.B)  && isGrounded && !isCrouch && !isRoll)
        {
            MusicManager.Instance.playSFX("sword");
            animator.SetTrigger("isAttack3");
            PerformAttack(3);
            StartCoroutine(SetAttackState(0.3f));
        }

    }
    IEnumerator SetAttackState(float duration)
    {
        isAttack = true;
        yield return new WaitForSeconds(duration);
        isAttack = false;
    }

    public void updateAniSkill1()
    {
        animator.SetTrigger("isSkill1");
    }
    public void updateAniSkill2()
    {
        animator.SetTrigger("isSkill2");
    }    
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isDie",true);
        StartCoroutine(WaitForDeathAnimation() );
    }
    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        spriteRenderer.enabled = false;
        this.enabled = false;
        if(life > 0)
        {
            life--;
            ManagerScene1.Instance.UpdateLives();
            Debug.Log("Player Respawn. Remaining lives: " + life);
            StartCoroutine(RespawnDelay());
        }
        else
        {
            Debug.Log("Game Over");
        }
        
    }
    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(2f);
        transform.position = PlayerRespawn.instance.GetCheckpoint();        
        
        spriteRenderer.enabled = true;
        this.enabled = true;        
        
        PlayerHealth.instance.ResetHealth();
        animator.SetBool("isDie", false);
        animator.SetTrigger("isRespawn");

        isDead = false;
    }
    void Flip()
    {
        if (move.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (move.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if(isJump)
            {
                isJump = false;
                animator.SetBool("isJump", false);
                animator.SetTrigger("isFall");
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isDead && !isAttack)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                StartCoroutine(Flash());
                PlayerHealth.instance.TakeDamage(5);
                lastDamageTime = Time.time;

            }
        }
    }

    private IEnumerator Flash()
    {
        spriteRenderer.color = Color.red;
        animator.SetTrigger("isHurt");
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }
    public void TriggerFlash()
    {
        StartCoroutine(Flash());
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    IEnumerator Roll()
    {
        isRoll = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(new Vector2(transform.localScale.x * RollForce, 0), ForceMode2D.Impulse);
        animator.SetTrigger("isRoll");
        yield return new WaitForSeconds(0.3f);
        rb.linearVelocity = Vector2.zero;
        isRoll = false;

    }
    IEnumerator Slide()
    {
        isSlide = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(new Vector2(transform.localScale.x * SlideForce, 0), ForceMode2D.Impulse);
        animator.SetTrigger("isSlide");
        yield return new WaitForSeconds(0.3f);
        rb.linearVelocity = Vector2.zero;
        isSlide = false;
    }
    public void PerformAttack(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            BatController bat = enemy.GetComponent<BatController>();
            if (bat != null) bat.TakeDamage(damage);

            DinoScript dino = enemy.GetComponent<DinoScript>();
            if (dino != null) dino.TakeDamage(damage);

            BeastGhostController beastgost = enemy.GetComponent<BeastGhostController>();
            if(beastgost != null) beastgost.TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Die();
        }
    }

}



