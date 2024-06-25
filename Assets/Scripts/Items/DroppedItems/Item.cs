using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField]
    public Sprite itemSprite;
    [SerializeField]
    public string itemName;
    [SerializeField]
    public int dropChance;
    // Start is called before the first frame update

    static private float DAMAGE = 3f;
    static private float SMALL_HEALTH = 3f;
    static private float LARGE_HEALTH = 8f;
    static private float BULLET_SPEED = 1f;


    private void Awake()
    {
        Item item = this;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemName)
            {
                case "Small bandage":
                    Player.Instance.AddHealth(SMALL_HEALTH);
                    break;
                case "Large bandage":
                    Player.Instance.AddHealth(SMALL_HEALTH);
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
                default:
                    break;
            }
        }
    }
}
