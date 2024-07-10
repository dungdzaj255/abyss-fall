using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    public Transform player;
    public float parallaxEffectMultiplier = 0.5f;

    private Tilemap tilemap;
    private float tilemapHeight;
    private Vector3 initialPlayerPosition;
    private float initialBackgroundY;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapHeight = CalculateTilemapHeight(tilemap);
        initialPlayerPosition = player.position;
        initialBackgroundY = tilemap.transform.position.y;
    }

    void Update()
    {
        float playerMoveDown = initialPlayerPosition.y - player.position.y;

        if (playerMoveDown > 0)
        {
            float backgroundMoveY = playerMoveDown * parallaxEffectMultiplier;
            Vector3 newPosition = new Vector3(tilemap.transform.position.x, initialBackgroundY - backgroundMoveY, tilemap.transform.position.z);

            // Only update position if it has changed
            if (tilemap.transform.position != newPosition)
            {
                tilemap.transform.position = newPosition;
            }

            if (playerMoveDown >= tilemapHeight)
            {
                RecycleBackground();
                initialPlayerPosition = player.position;
                initialBackgroundY = tilemap.transform.position.y;
            }
        }
    }

    void RecycleBackground()
    {
        tilemap.transform.position -= new Vector3(0, tilemapHeight, 0);
    }

    float CalculateTilemapHeight(Tilemap tilemap)
    {
        // Calculate the height of the Tilemap based on its bounds
        BoundsInt bounds = tilemap.cellBounds;
        return bounds.size.y * tilemap.cellSize.y;
    }
}
