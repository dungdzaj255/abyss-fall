using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* healthbar */
    [SerializeField] private Bar healthBar;

    //======================
    public float moveSpeed = 5f;
    
    public float jumpPower = 5f;

    public float headDamage = 100f;

    public float maxHealth = 100f;
    private float currentHealth;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;

    private Vector2 movement;
    bool isGrounded = false;
    private bool isDead = false;

    public static Player Instance;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if(Instance == null)
        {
            Instance = this;
        }
        /* healthbar */
        healthBar.InitBar(Int32.Parse(maxHealth + ""));
        healthBar.SetMax(Int32.Parse(maxHealth + ""));
        //======================
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input (A/D or Left/Right arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");

        // Set animation parameters
        if (movement.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Flip the player sprite based on the movement direction
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }


        /* healthbar */
        if (Input.GetKeyDown(KeyCode.RightAlt)) {
            TakeDamage(10f);
        }
        //======================
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public void AddHealth(float health)
    {
        currentHealth += health;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }


        /* healthbar */
        healthBar.SetCurrent(Int32.Parse(currentHealth + ""));
        //======================
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) {
            currentHealth = 0;
        }
        /* healthbar */
        healthBar.SetCurrent(Int32.Parse(currentHealth + ""));
        //======================
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;  // Prevent multiple death triggers
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", !isGrounded);
        }
        else if (collision.CompareTag("EnemyLevel1") && !isGrounded)
        {
            EL1Health eL1Health = collision.gameObject.GetComponent<EL1Health>();
            if (eL1Health != null)
            {
                eL1Health.TakeDamage(headDamage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyLevel1"))
        {
            TakeDamage(50);
            animator.SetBool("takeDamage", true);
        }
        else
        {
            animator.SetBool("takeDamage", false);
        }
    }


}
