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

    static private float DAMAGE = 3;
    static private float SMALL_HEALTH = 3;
    static private float LARGE_HEALTH = 8;


    private void Awake()
    {
        Item item = this;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
