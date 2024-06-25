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
}
