using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratePlatform : MonoBehaviour
{
    public Tilemap platformTilemap;
    public PlatformObjectPool platformObjectPool; // Reference to your platform object pool
    public Tilemap[] platformTilemapPrefabs; // Array of Tilemap prefabs

    public int minTilemapsPerLine = 3; // Minimum number of tilemaps per line
    public int maxTilemapsPerLine = 3; // Maximum number of tilemaps per line
    public float distanceBetweenTilemaps = 1f; // Vertical distance between each generated Tilemap
    public float distanceReductionPerLine = 0.1f; // Amount to reduce the distance between each line
    public int maxWidth = 7; // Maximum width of generated Tilemaps
    public Transform player; // Reference to the player
    public float scrollSpeed = 5f; // Speed at which platforms scroll up

    private Vector3 initialPlayerPosition;
    private Vector3Int lastGeneratedPosition;

    void Start()
    {
        initialPlayerPosition = player.position;
        lastGeneratedPosition = platformTilemap.origin - new Vector3Int(0, 1, 0); // Start just below the initial origin
    }

    void Update()
    {
        if (player.position.y < initialPlayerPosition.y)
        {
            Vector3 move = new Vector3(0, scrollSpeed * Time.deltaTime, 0);
            platformTilemap.transform.position += move;

            // Check if we need to generate more platforms below
            float distanceToGenerate = Mathf.Abs(player.position.y - initialPlayerPosition.y);
            while (distanceToGenerate > distanceBetweenTilemaps)
            {
                GeneratePlatforms();
                distanceToGenerate -= distanceBetweenTilemaps;
            }
        }
    }

    void GeneratePlatforms()
    {
        Vector3Int currentPosition = lastGeneratedPosition;
        float currentDistanceBetweenTilemaps = distanceBetweenTilemaps;

        int numOfTilemapsToGenerate = Random.Range(minTilemapsPerLine, maxTilemapsPerLine + 1);

        for (int i = 0; i < numOfTilemapsToGenerate; i++)
        {
            // Get a random tilemap prefab
            int prefabIndex = Random.Range(0, platformTilemapPrefabs.Length);
            Tilemap prefabTilemap = platformTilemapPrefabs[prefabIndex];

            if (prefabTilemap.size.x > maxWidth)
            {
                Debug.LogWarning($"Tilemap prefab {prefabTilemap.name} width exceeds maxWidth. Adjusting width.");
                prefabTilemap.size = new Vector3Int(maxWidth, prefabTilemap.size.y, prefabTilemap.size.z);
            }

            // Calculate starting X position based on tilemap size and alignment
            int startX = currentPosition.x + (platformTilemap.size.x - prefabTilemap.size.x) / 2;
            int xOffset = i * (maxWidth + 1); // Adjust as needed
            Vector3Int platformPosition = new Vector3Int(startX + xOffset, currentPosition.y, currentPosition.z);

            // Example: Ensure platform aligns properly with tilemap grid
            platformPosition.x = Mathf.RoundToInt(platformPosition.x); // Round to nearest integer

            PlaceTilemap(prefabTilemap, platformPosition);
        }

        lastGeneratedPosition -= new Vector3Int(0, platformTilemapPrefabs[0].size.y + Mathf.RoundToInt(currentDistanceBetweenTilemaps), 0);

        // Reduce the distance between tilemaps for the next line
        currentDistanceBetweenTilemaps = Mathf.Max(0, currentDistanceBetweenTilemaps - distanceReductionPerLine);
    }

    void PlaceTilemap(Tilemap tilemapPrefab, Vector3Int position)
    {
        BoundsInt bounds = tilemapPrefab.cellBounds;
        TileBase[] allTiles = tilemapPrefab.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int prefabPosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemapPrefab.GetTile(prefabPosition);

                if (tile != null)
                {
                    Vector3Int tilePosition = new Vector3Int(position.x + x, position.y + y, position.z);
                    platformTilemap.SetTile(tilePosition, tile);
                }
            }
        }
    }
}
