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
    private Vector3 lastPosition;
    private bool isMovingBack = false;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTargetPosition();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTargetPosition();
        CheckDirectionAndFlip();
    }

    void SetRandomTargetPosition()
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
            speed = 0;
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
}
