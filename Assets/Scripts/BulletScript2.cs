using UnityEngine;

public class BulletScript2 : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 10f;
    private Vector3 direction = Vector3.right;
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        Vector3 scale = transform.localScale;
        scale.x = direction.x >= 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
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
        BulletPool.Instance.ReturnBullet2(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BatController>()?.TakeDamage(10);
            other.GetComponent<DinoScript>()?.TakeDamage(10);
            other.GetComponent<BeastGhostController>()?.TakeDamage(10);
            gameObject.SetActive(false);
        }
    }
}
