using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField]
    private float minX = -8.31f;
    [SerializeField]
    private float maxX = 6.24f;
    [SerializeField]
    private float minY = -2.95f;
    [SerializeField]
    private float maxY = -1.42f;
    [SerializeField]
    private float speed;
    [SerializeField]
    public float damageTouch;
    [SerializeField]
    public int POINTS;
    private Vector3 lastPosition;
    private bool isMovingBack = false;
    [SerializeField]
    private bool isAbleAttack = false;
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private EnemyAttackPool attackPool;
    [SerializeField]
    private float attackInterval = 2.5f;
    private float timer = 0f;
    private int currentAttackCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTargetPosition();
        lastPosition = transform.position;

        if (attackPool == null)
        {
            Debug.LogError("attackPool is not assigned in " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTargetPosition();
        CheckDirectionAndFlip();
    }

    public void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }

    void MoveToTargetPosition()
    {
        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth.GetCurrentHealth() <= 0)
        {
            isMoving = false;
        }
        else if (enemyHealth.GetCurrentHealth() >= 1)
        {
            isMoving = true;
        }
        if (isMovingBack)
        {
            targetPosition = -targetPosition;
            isMovingBack = false;
        }
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void CheckDirectionAndFlip()
    {
        if (transform.position.x > lastPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < lastPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        lastPosition = transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Enemy lv1 va cham voi Player");
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(damageAmount);
            //}
        }
        else if (collision.gameObject.tag == "Platform")
        {
            FlipDirection();
        }

    }
    void FlipDirection()
    {
        isMovingBack = true;
    }
    public void EnableAttacking(bool enable)
    {
        isAbleAttack = enable;
    }

    public IEnumerator SpawnAttack(int deadCount)
    {
        deadCount = currentAttackCount;

        while (isAbleAttack)
        {
            yield return new WaitForSeconds(attackInterval);
            for (int i = 0; i < currentAttackCount; i++)
            {
                if (isAbleAttack == true)
                {
                    Debug.Log("1");

                    if (attackPool == null)
                    {
                        Debug.LogError("attackPool is not assigned!");
                        yield break; 
                    }

                    GameObject attack = attackPool.GetPooledObject();
                    Debug.Log("2");
                    if (attack != null)
                    {
                        Debug.Log("3");
                        attack.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                        Debug.Log("4");
                        attack.SetActive(true);
                        Debug.Log("5");
                    }
                }
                yield return null;
            }
        }
    }
}
