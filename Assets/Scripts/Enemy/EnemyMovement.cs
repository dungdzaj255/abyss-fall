using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField]
    private float minX = -6.46f;
    [SerializeField]
    private float maxX = 6.34f;
    [SerializeField]
    private float minY = -9.36f;
    [SerializeField]
    private float maxY = -9.06f;
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
    private EnemyAttackPool attackPool;
    [SerializeField]
    private float attackInterval = 2.5f;
    private int currentAttackCount = 1;


    // Start is called before the first frame update

    void Start()
    {
        SetRandomTargetPosition();
        lastPosition = transform.position;
        if (transform.name.Equals("Level1(Clone)"))
        {
            attackPool = GameObject.Find("EL1Pool").GetComponent<EnemyAttackPool>();
        }
        else if (transform.name.Equals("Level2(Clone)"))
        {
            attackPool = GameObject.Find("EL2Pool").GetComponent<EnemyAttackPool>();
        }
        else if (transform.name.Equals("Level3(Clone)"))
        {
            attackPool = GameObject.Find("EL3Pool").GetComponent<EnemyAttackPool>();
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
        targetPosition = new Vector2(randomX, randomY);
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
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void CheckDirectionAndFlip()
    {
        if (transform.position.x > lastPosition.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x < lastPosition.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        lastPosition = transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
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
            GetComponent<Animator>().SetBool("IsAttacking", true);
            for (int i = 0; i < currentAttackCount; i++)
            {
                if (isAbleAttack == true)
                {

                    GameObject attack = attackPool.GetPooledObject();
                    if (attack != null)
                    {
                        attack.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
                        attack.SetActive(true);
                    }
                }
                yield return null;
            }

            // Set IsAttacking to false after the attack cycle
            GetComponent<Animator>().SetBool("IsAttacking", false);

            // Wait for the attack interval before starting the next attack cycle
            yield return new WaitForSeconds(attackInterval);
        }
    }



}
