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
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
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
        if (animator != null)
        {
            animator.SetTrigger("death");
            StartCoroutine(DelayedActions());
        }
    }

    IEnumerator DelayedActions()
    {
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
