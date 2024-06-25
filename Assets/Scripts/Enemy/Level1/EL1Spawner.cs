using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EL1Spawner : MonoBehaviour
{
    public static EL1Spawner instance;
    private List<GameObject> pooledEnemies = new List<GameObject>();
    [SerializeField] private int amountToPool = 5;
    [SerializeField] private GameObject el1Prefab;
    [SerializeField] private float spawnInterval = 3f;
    private float spawnTimer;
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
            EL1Health enemyHealth = enemy.GetComponent<EL1Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ResetEnemyHealth();
            }
        }
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
        return new Vector3(-1.18f, -6.0f, -0.03689937f);
    }
}