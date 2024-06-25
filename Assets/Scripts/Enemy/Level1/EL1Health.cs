using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EL1Health : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 1;
    private int currentHealth;
    private EL1Spawner enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
        currentHealth = maxHealth;
        //ScoreController.instance.AddPoint();
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void ResetEnemyHealth()
    {
        currentHealth = maxHealth;
    }
}
