using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EL1Health : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 1;
    private float currentHealth;
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
    public void TakeDamage(float damageAmount)
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
