using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformPrefabEntry
{
    public GameObject prefab;
    public int initialPoolSize = 10;
}

public class PlatformObjectPool : MonoBehaviour
{
    public List<PlatformPrefabEntry> platformPrefabs = new List<PlatformPrefabEntry>();

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    void Start()
    {
        // Initialize object pools for each platform prefab
        foreach (var entry in platformPrefabs)
        {
            InitializePool(entry.prefab, entry.initialPoolSize);
        }
    }

    void InitializePool(GameObject prefab, int initialSize)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject platform = Instantiate(prefab, transform);
            platform.SetActive(false);
            objectPool.Enqueue(platform);
        }

        poolDictionary.Add(prefab, objectPool);
    }

    public GameObject GetRandomPlatformFromPool()
    {
        // Select a random platform prefab
        int randomIndex = Random.Range(0, platformPrefabs.Count);
        GameObject prefab = platformPrefabs[randomIndex].prefab;

        return GetPlatformFromPool(prefab);
    }

    public GameObject GetPlatformFromPool(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            InitializePool(prefab, 1); // Create a new pool for the prefab if it doesn't exist
        }

        Queue<GameObject> objectPool = poolDictionary[prefab];

        if (objectPool.Count == 0)
        {
            // Expand the pool if needed (optional)
            GameObject platform = Instantiate(prefab, transform);
            platform.SetActive(false);
            objectPool.Enqueue(platform);
        }

        GameObject platformInstance = objectPool.Dequeue();
        platformInstance.SetActive(true);
        return platformInstance;
    }

    public void ReturnPlatformToPool(GameObject platform)
    {
        platform.SetActive(false);
        foreach (var entry in platformPrefabs)
        {
            if (platform.CompareTag(entry.prefab.tag))
            {
                poolDictionary[entry.prefab].Enqueue(platform);
                break;
            }
        }
    }
}
