using UnityEngine;

public class BeastGhostBullet : MonoBehaviour
{
    public float lifeTime = 2f;
    private float speed = 5f;
    private Vector3 direction = Vector3.right;
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

    }
    void OnEnable()
    {
        Invoke("DisableBullet", lifeTime);
    }

    void DisableBullet()
    {
        BulletPool.Instance.ReturnBeastGhostBullet(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(30);
            other.GetComponent<PlayerController>().TriggerFlash();
            gameObject.SetActive(false);
        }
    }
}
