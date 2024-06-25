using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EL1Movement : MonoBehaviour
{
    private Vector3 targetPosition;
    private float minX = -8.31f;
    private float maxX = 6.24f;
    private float minY = -2.95f;
    private float maxY = -1.42f;
    private float speed = 2f;
    private Vector3 lastPosition;
    public Weapon weapon;

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
        if (transform.position.x > lastPosition.x) // Moving right
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip the prefab
        }
        else if (transform.position.x < lastPosition.x) // Moving left
        {
            transform.localScale = new Vector3(1, 1, 1); // Keep the prefab original
        }
        lastPosition = transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy lv1 va cham voi Player");
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(damageAmount);
            //}
        }

    }
}
