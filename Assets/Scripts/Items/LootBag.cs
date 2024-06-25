using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    [SerializeField]
    private GameObject droppedItemPrefab;
    [SerializeField]
    private List<Item> itemList = new List<Item>();
    // Start is called before the first frame update

    Item GetDropItem()
    {
        int rand = Random.Range(1, 100);
        List<Item> possibleItems = new List<Item>();
        foreach (var item in itemList)
        {
            if (rand <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            Item droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }

    public void InstantiateItem(Vector2 spawnPosition)
    {
        Item droppedItem = GetDropItem();
        if (droppedItem != null)
        {
            GameObject item = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            float dropForce = 300f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f));
            item.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce);
        }
    }
    // Update is called once per frame
}
