using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLooping : MonoBehaviour
{
    public Transform player;
    public Transform background1;
    public Transform background2; 

    private float backgroundHeight; 

    void Start()
    {
        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        if (player.position.y < background1.position.y - backgroundHeight)
        {
            LoopBackground(background1);
        }
        else if (player.position.y < background2.position.y - backgroundHeight)
        {
            LoopBackground(background2);
        }
    }

    void LoopBackground(Transform background)
    {
        background.position = new Vector3(background.position.x, background.position.y + 2 * backgroundHeight, background.position.z);
    }
}
