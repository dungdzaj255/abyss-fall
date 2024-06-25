using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite idleSprite;
    [SerializeField]
    private Sprite shootSprite;
    [SerializeField]
    private GameObject flash;
    [Header("Bullet")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private LayerMask bulletLayerMask;
    [SerializeField]
    private BulletPool BulletPool;

    [Header("Characteristics")]
    [SerializeField]
    public float damage = 10f;
    [SerializeField]
    public int bulletAmount = 4;
    [SerializeField]
    public float bulletSpeed = 5f;
    [SerializeField]
    public float recoil = 2f;
    [SerializeField]
    public float fireRate = 0.3f;

    private float nextFireTime = 0f;
    private bool canShoot = false;
    private int currentBulletAmount { get; set; } = 0;

    public static Weapon Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        BulletPool.InitializePool(bulletAmount);
        currentBulletAmount = bulletAmount;
        Iddle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            Shoot();
        }
    }
    public void AddDamage(float newDamage)
    {
        damage += newDamage;
    }

    public void AddBulletSpeed(float speed)
    {
        bulletSpeed += speed;
    }

    public void AddBullet()
    {
        bulletAmount++;
        currentBulletAmount++;
        BulletPool.AddBullet();
    }

    public void Iddle()
    {
        spriteRenderer.sprite = idleSprite;
        flash.SetActive(false);
    }

    public void Shoot()
    {
        if (canShoot && currentBulletAmount > 0 && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            --currentBulletAmount;
            spriteRenderer.sprite = shootSprite;
            flash.SetActive(true);
            GameObject bullet = BulletPool.GetBullet();
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
                bullet.GetComponent<CapsuleCollider2D>().enabled = true;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rb.gravityScale = 0;
                //độ giật súng
                GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * recoil, ForceMode2D.Impulse);
            }
        }
    }

    public void ReloadBullets()
    {
        currentBulletAmount = bulletAmount;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            canShoot = false;
            ReloadBullets();
            Iddle();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            Invoke("setCanShoot", 0.5f);
        }
    }

    private void setCanShoot()
    {
        canShoot = true;
    }

}
