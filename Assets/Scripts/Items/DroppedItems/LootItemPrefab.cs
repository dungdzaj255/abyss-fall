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
    [SerializeField]
    private int POINTS;

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
                    break;
                case "Large bandage":
                    Player.Instance.AddHealth(LARGE_HEALTH);
                    break;
                case "Damage":
                    Weapon.Instance.AddDamage(DAMAGE);
                    break;
                case "Bullet":
                    Weapon.Instance.AddBullet();
                    break;
                case "Bullet speed":
                    Weapon.Instance.AddBulletSpeed(BULLET_SPEED);
                    break;
                //case "Points":
                //    PointSystem.instance.AddPoint(POINTS);
                //    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
