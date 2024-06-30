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
        // Calculate how much the player has moved down
        float playerMoveDown = initialPlayerPosition.y - player.position.y;

        if (playerMoveDown > 0)
        {
            // Move the background based on the player's movement
            float backgroundMoveY = playerMoveDown * parallaxEffectMultiplier;
            tilemap.transform.position = new Vector3(tilemap.transform.position.x, initialBackgroundY - backgroundMoveY, tilemap.transform.position.z);

            // Recycle the background when it moves out of the camera view
            if (initialPlayerPosition.y - player.position.y >= tilemapHeight)
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
