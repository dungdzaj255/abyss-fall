using System.Collections;
using System.Collections.Generic;
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
    public Bullet bulletPrefab;
    [SerializeField]
    public LayerMask bulletLayerMask;

    private int shootLength = 1;
    private int shootLine = 1;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    public void AddShootLength()
    {
        shootLength++;
    }

    public void AddShootLine()
    {
        shootLine++;
    }

    private void Iddle()
    {
        spriteRenderer.sprite = idleSprite;
        flash.SetActive(false);
    }

    private void Shoot()
    {
        spriteRenderer.sprite = shootSprite;
        flash.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            Iddle();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            Shoot();
        }
    }


}
