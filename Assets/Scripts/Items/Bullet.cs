using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        this.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "EnemyLevel1")
        {
            EnemyHealth eL1Health = collision.gameObject.GetComponent<EnemyHealth>();
            if (eL1Health != null)
            {
                eL1Health.TakeDamage(Weapon.Instance.damage);
            }
        }
    }
}
