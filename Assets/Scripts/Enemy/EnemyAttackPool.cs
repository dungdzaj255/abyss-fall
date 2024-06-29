using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPool : MonoBehaviour
{
    public static EnemyAttackPool instance;
    private List<GameObject> enemyAttackObjects = new List<GameObject>();
    [SerializeField]
    private int amountToPool;
    [SerializeField]
    private GameObject enemyAttackPrefab;
    private void Awake()
    {
        instance = this;
        InitializePool();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InitializePool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(enemyAttackPrefab);
            obj.SetActive(false);
            enemyAttackObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < enemyAttackObjects.Count; i++)
        {
            if (!enemyAttackObjects[i].activeInHierarchy)
            {
                return enemyAttackObjects[i];
            }
        }
        return null;
    }
    public void ReturnToPool(GameObject enemyAttack)
    {
        enemyAttack.SetActive(false);
        enemyAttack.transform.position = Vector3.zero;
        enemyAttackObjects.Add(enemyAttack);

    }
}
