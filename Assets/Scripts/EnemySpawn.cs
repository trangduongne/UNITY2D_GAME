
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public enum SpawnMode
    {
        Single,
        Batch
    }

    [System.Serializable]
    public class SpawnData
    {
        public GameObject enemyPrefab;
        public Transform spawnPoint;
    }


    public SpawnData[] spawnList;
    public float spawnInterval = 5f;
    public SpawnMode spawnMode = SpawnMode.Single;
    private int spawnIndex = 0;

    void Start()
    {
        switch (spawnMode)
        {
            case SpawnMode.Single:
                InvokeRepeating(nameof(SpawnOneFromList), 1f, spawnInterval);
                break;

            case SpawnMode.Batch:
                Invoke(nameof(SpawnEnemies), 1f);
                break;
        }
    }

    void SpawnOneFromList()
    {
        if (spawnList.Length == 0) return;

        SpawnData data = spawnList[spawnIndex];
        SpawnOneEnemy(data.enemyPrefab, data.spawnPoint);

        spawnIndex = (spawnIndex + 1) % spawnList.Length;
    }

    void SpawnEnemies()
    {
        foreach (SpawnData data in spawnList)
        {
            SpawnOneEnemy(data.enemyPrefab, data.spawnPoint);
        }
    }

    public void SpawnOneEnemy(GameObject prefab, Transform point)
    {
        GameObject enemy = Instantiate(prefab, point.position, Quaternion.identity);
    }
}


