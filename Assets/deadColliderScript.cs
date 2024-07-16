using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !Player.Instance.getIsGround())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Rigidbody2D rb = Player.Instance.GetComponent<Rigidbody2D>();
                enemyHealth.TakeDamage(Player.Instance.headDamage);
                rb.velocity = new Vector2(rb.velocity.x, Player.Instance.bounceForce);
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySFX(AudioManager.instance.collidingEnemysHead);
                }
            }
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("xxxxxxxxxxxxxxxxx");
            Animator animator = Player.Instance.GetComponent<Animator>();
            Player.Instance.setIsGround(true);
            animator.SetBool("isJumping", !Player.Instance.getIsGround());
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Animator animator = Player.Instance.GetComponent<Animator>();
    //    if (collision.gameObject.CompareTag("Platform"))
    //    {
    //        Player.Instance.isGrounded = true;
    //        animator.SetBool("isJumping", !Player.Instance.isGrounded);
    //    }

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy") && !Player.Instance.isGrounded)
    //    {
    //        Rigidbody2D rb = Player.Instance.GetComponent<Rigidbody2D>();
    //        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
    //        if (enemyHealth != null)
    //        {
    //            enemyHealth.TakeDamage(Player.Instance.headDamage);
    //            rb.velocity = new Vector2(rb.velocity.x, Player.Instance.bounceForce);
    //            if (AudioManager.instance != null)
    //            {
    //                AudioManager.instance.PlaySFX(AudioManager.instance.collidingEnemysHead);
    //            }
    //        }
    //    }
    //}
}
