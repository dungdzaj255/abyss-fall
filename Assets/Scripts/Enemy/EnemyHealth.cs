using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    private float currentHealth;
    private EnemySpawner enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        GetComponent<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        currentHealth = maxHealth;
        GetComponent<ItemBag>().InstantiateItem(transform.position);
        //ScoreController.Instance.AddPoint();
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void ResetEnemyHealth()
    {
        currentHealth = maxHealth;
    }
}
