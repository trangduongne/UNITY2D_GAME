using UnityEngine;

public class BatBulletScript : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 6f;
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
        BulletPool.Instance.ReturnBatBullet(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(20); 
            other.GetComponent <PlayerController>().TriggerFlash();
            gameObject.SetActive(false);
        }
    }
}
