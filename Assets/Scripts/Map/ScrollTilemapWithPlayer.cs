using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrollTilemapWithPlayer : MonoBehaviour
{
    public Transform player;
    public Tilemap backgroundTilemap;
    public Tilemap wallTilemap;
    public float parallaxEffectMultiplier = 0.5f;

    private Vector3 initialPlayerPosition;
    private Vector3 initialBackgroundPosition;
    private Vector3 initialWallPosition;

    void Start()
    {
        initialPlayerPosition = player.position;
        initialBackgroundPosition = backgroundTilemap.transform.position;
        initialWallPosition = wallTilemap.transform.position;
    }

    void Update()
    {
        float playerMoveDown = initialPlayerPosition.y - player.position.y;

        if (playerMoveDown > 0)
        {
            float backgroundMoveY = playerMoveDown * parallaxEffectMultiplier;
            Vector3 newBackgroundPosition = initialBackgroundPosition + Vector3.down * backgroundMoveY;
            backgroundTilemap.transform.position = newBackgroundPosition;

            float wallMoveY = playerMoveDown;  // Adjust as needed based on your game's design
            Vector3 newWallPosition = initialWallPosition + Vector3.down * wallMoveY;
            wallTilemap.transform.position = newWallPosition;
        }
    }
}
