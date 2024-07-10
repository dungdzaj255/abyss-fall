using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    private List<GameObject> pooledEnemies = new List<GameObject>();
    [SerializeField] private int amountToPool = 5;
    [SerializeField] private GameObject el1Prefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform[] spawnPoints;
    private float spawnTimer;
    private int deadCount=1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(el1Prefab);
            obj.SetActive(false);
            pooledEnemies.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }
    void SpawnEnemy()
    {
        GameObject enemy = GetPooledEnemy();
        if (enemy != null)
        {
            Vector3 spawnPosition = GetFixedSpawnPosition();
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ResetEnemyHealth();
                enemy.GetComponent<EnemyMovement>().EnableAttacking(true);
                StartCoroutine(enemy.GetComponent<EnemyMovement>().SpawnAttack(deadCount));
            }

        }
    }
    public void IncreaseDeadCount()
    {
        deadCount++;
    }

    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }
    Vector3 GetFixedSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
}
