using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 10f;
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
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BatController>()?.TakeDamage(5); 
            other.GetComponent<DinoScript>()?.TakeDamage(5);
            other.GetComponent<BeastGhostController>()?.TakeDamage(5);
            gameObject.SetActive(false);
        }
    }

}
