using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject BatPrefab;
    public int batPool = 10;
    private Queue<GameObject> BatBool = new Queue<GameObject>();
    public static EnemyPool Instance;


    public GameObject DinoPrefab;
    public int dinoPool = 10;
    private Queue<GameObject> DinoPool = new Queue<GameObject>();

    public GameObject BeastGhostoPrefab;
    public int beastGhostPool = 3;
    private Queue<GameObject> BeastGhostPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeBatPool();
        InitializeDinoPool();
        InitializebeastGhostPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitializeBatPool()
    {
        for(int i = 0; i < batPool; i++)
        {
            GameObject bat = Instantiate(BatPrefab);
            bat.SetActive(false);
            BatBool.Enqueue(bat);
        }    
    }
    void InitializeDinoPool()
    {
        for (int i = 0; i < dinoPool; i++)
        {
            GameObject dino = Instantiate(DinoPrefab);
            dino.SetActive(false);
            DinoPool.Enqueue(dino);
        }
    }
    void InitializebeastGhostPool()
    {
        for (int i = 0; i < beastGhostPool; i++)
        {
            GameObject beastGhost = Instantiate(BeastGhostoPrefab);
            beastGhost.SetActive(false);
            BeastGhostPool.Enqueue(beastGhost);
        }
    }    
    public GameObject GetBat(Vector3 position)
    {
        GameObject bat;
        if (BatBool.Count > 0)
        {
            bat = BatBool.Dequeue();
        }
        else
        {
            bat = Instantiate(BatPrefab);
        }

        bat.transform.position = position;
        bat.SetActive(true);
        return bat;
    }
    public GameObject GetDino(Vector3 position)
    {
        GameObject dino;
        if (DinoPool.Count > 0)
        {
            dino = DinoPool.Dequeue();
        }
        else
        {
            dino = Instantiate(DinoPrefab);
        }

        dino.transform.position = position;
        dino.SetActive(true);
        return dino;
    }
    public GameObject GetBeastGhost(Vector3 position)
    {
        GameObject beastGhost;
        if (BeastGhostPool.Count > 0)
        {
            beastGhost = DinoPool.Dequeue();
        }
        else
        {
            beastGhost = Instantiate(DinoPrefab);
        }

        beastGhost.transform.position = position;
        beastGhost.SetActive(true);
        return beastGhost;
    }

    public void ReturnBat(GameObject bat)
    {
        bat.SetActive(false);
        BatBool.Enqueue(bat);
    }
    
    public void ReturnDino(GameObject dino)
    {
        dino.SetActive(false);
        DinoPool.Enqueue(dino);
    }
    public void ReturnBeastGhost(GameObject beastGhost)
    {
        beastGhost.SetActive(false);
        DinoPool.Enqueue(beastGhost);
    }
}
