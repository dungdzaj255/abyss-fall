using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootItemPrefab : MonoBehaviour
{
    static private float DAMAGE = 3f;
    static private float SMALL_HEALTH = 3f;
    static private float LARGE_HEALTH = 8f;
    static private float BULLET_SPEED = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        }
        if (other.transform.CompareTag("Player"))
        {
            switch (this.name)
            {
                case "Small bandage":
                    Player.Instance.AddHealth(SMALL_HEALTH);
                    Debug.Log($"Add health +{SMALL_HEALTH}");
                    break;
                case "Large bandage":
                    Player.Instance.AddHealth(LARGE_HEALTH);
                    Debug.Log($"Add health +{LARGE_HEALTH}");
                    break;
                case "Damage":
                    Weapon.Instance.AddDamage(DAMAGE);
                    Debug.Log($"Add damage +{DAMAGE}");
                    break;
                case "Bullet":
                    Weapon.Instance.AddBullet();
                    Debug.Log($"Add bullet amount +1");
                    break;
                case "Bullet speed":
                    Weapon.Instance.AddBulletSpeed(BULLET_SPEED);
                    Debug.Log($"Add bullet speed +{BULLET_SPEED}");
                    break;
                case "Points":
                    //UIController.Instance.AddPoint();
                    Debug.Log($"Add bullet speed +{BULLET_SPEED}");
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
