using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject healPrefab;
    public GameObject BatBulletPrefab;
    public GameObject BeastGhostBulletPrefab;
    public int poolSize = 10;
    public int poolSize2 = 10;
    public int healPoolSize = 5;
    public int BatBulletPoolSize = 5;
    public int BeastGhostPoolSize = 5;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private Queue<GameObject> bulletPool2 = new Queue<GameObject>();    
    private Queue<GameObject> healPool = new Queue<GameObject>();
    private Queue<GameObject> BatBulletPool = new Queue<GameObject>();
    private Queue<GameObject> beastGhostBulletPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
        InitializePool2();
        InitializeHealPool();
        InitializeBatBulletPool();
        InitializeBeastGhostBulletPool();
    }
    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    void InitializePool2()
    {
        for (int i = 0; i < poolSize2; i++)
        {
            GameObject bullet2 = Instantiate(bulletPrefab2);
            bullet2.SetActive(false);
            bulletPool2.Enqueue(bullet2);
        }
    }
    void InitializeHealPool()
    {
        for (int i = 0; i < healPoolSize; i++)
        {
            GameObject heal = Instantiate(healPrefab);
            heal.SetActive(false);
            healPool.Enqueue(heal);
        }
    }
    void InitializeBatBulletPool()
    {
        for (int i = 0; i < BatBulletPoolSize; i++)
        {
            GameObject BatBullet = Instantiate(BatBulletPrefab);
            BatBullet.SetActive(false);
            BatBulletPool.Enqueue(BatBullet);
        }
    }
    void InitializeBeastGhostBulletPool()
    {
        for (int i = 0; i < BeastGhostPoolSize; i++)
        {
            GameObject beastGhost = Instantiate(BeastGhostBulletPrefab);
            beastGhost.SetActive(false);
            beastGhostBulletPool.Enqueue(beastGhost);
        }
    }    
    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }
    public GameObject GetBullet2()
    {
        if (bulletPool2.Count > 0)
        {
            GameObject bullet2 = bulletPool2.Dequeue();
            bullet2.SetActive(true);
            return bullet2;
        }
        else
        {
            GameObject bullet2 = Instantiate(bulletPrefab2);
            return bullet2;
        }
    }
    public GameObject GetHeal()
    {
        if (healPool.Count > 0)
        {
            GameObject heal = healPool.Dequeue();
            heal.SetActive(true);
            return heal;
        }
        else
        {
            GameObject heal = Instantiate(healPrefab);
            return heal;
        }
    }
    public GameObject GetBatBullet()
    {
        if (BatBulletPool.Count > 0)
        {
            GameObject BatBullet = BatBulletPool.Dequeue();
            BatBullet.SetActive(true);
            return BatBullet;
        }
        else
        {
            GameObject BatBullet = Instantiate(BatBulletPrefab);
            return BatBullet;
        }
    }
    public GameObject GetBeastGhostBullet()
    {
        if (beastGhostBulletPool.Count > 0)
        {
            GameObject BeastGhost = beastGhostBulletPool.Dequeue();
            BeastGhost.SetActive(true);
            return BeastGhost;
        }
        else
        {
            GameObject BeastGhost = Instantiate(BeastGhostBulletPrefab);
            return BeastGhost;
        }
    }
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
    public void ReturnBullet2(GameObject bullet2)
    {
        bullet2.SetActive(false);
        bulletPool2.Enqueue(bullet2);
    }
    public void ReturnHeal(GameObject heal)
    {
        heal.SetActive(false);
        healPool.Enqueue(heal);
    }
    public void ReturnBatBullet(GameObject batBullet)
    {
        batBullet.SetActive(false);
        BatBulletPool.Enqueue(batBullet);
    }
    public void ReturnBeastGhostBullet(GameObject beastGhostBullet)
    {
        beastGhostBullet.SetActive(false);
        BatBulletPool.Enqueue(beastGhostBullet);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
