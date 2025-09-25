using UnityEngine;

public class HealPrefabScript : MonoBehaviour
{
    private float lifeTime = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        Invoke("DisableHeal", lifeTime);
    }

    void DisableHeal()
    {
        BulletPool.Instance.ReturnHeal(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
