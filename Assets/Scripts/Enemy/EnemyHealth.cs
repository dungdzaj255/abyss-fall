using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (AudioManager.instance != null) {
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyDeath);
        }
        GetComponent<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(0.5f);

        if (gameObject != null)
        {
            gameObject.SetActive(false);
            currentHealth = maxHealth;
            GetComponent<ItemBag>().InstantiateItem(transform.position);

            if (PointSystem.instance != null && GetComponent<EnemyMovement>() != null)
            {
                PointSystem.instance.AddPoint(gameObject.GetComponent<EnemyMovement>().POINTS);
            }

            if (GetComponent<EnemySpawner>() != null)
            {
                GetComponent<EnemySpawner>().IncreaseDeadCount();
            }

            if (GetComponent<EnemyMovement>() != null)
            {
                GetComponent<EnemyMovement>().EnableAttacking(false);
            }
        }
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
