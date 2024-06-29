using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float enemyAttackDamageAmount;
    private Vector3 moveDirection;
    private Transform enemyTransform;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = Vector3.up;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        CheckOutOfBounds();

    }
    void Move()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    void CheckOutOfBounds()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Player")
        {
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyAttackDamageAmount);
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, boxCollider);
        }

    }

}
