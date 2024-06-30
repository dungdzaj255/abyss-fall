using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratePlatform : MonoBehaviour
{
    public Tilemap platformTilemap;
    public Tilemap[] platformTilemapPrefabs; // Array of Tilemap prefabs

    public int linesToGenerate = 3; // Number of lines of tilemaps to generate
    public int minTilemapsPerLine = 2; // Minimum number of tilemaps per line
    public int maxTilemapsPerLine = 3; // Maximum number of tilemaps per line
    public float distanceBetweenTilemaps = 1f; // Vertical distance between each generated Tilemap
    public float distanceReductionPerLine = 0.1f; // Amount to reduce the distance between each line
    public int maxWidth = 5; // Maximum width of generated Tilemaps
    public Transform player; // Reference to the player
    public float scrollSpeed = 2f; // Speed at which platforms scroll up

    private Vector3 initialPlayerPosition;

    void Start()
    {
        initialPlayerPosition = player.position;
        GeneratePlatforms();
    }

    void Update()
    {
        if (player.position.y < initialPlayerPosition.y)
        {
            Vector3 move = new Vector3(0, scrollSpeed * Time.deltaTime, 0);
            platformTilemap.transform.position += move;
        }
    }

    void GeneratePlatforms()
    {
        Vector3Int platformPosition = platformTilemap.origin;
        Vector3Int currentPosition = platformPosition - new Vector3Int(0, 1, 0);
        float currentDistanceBetweenTilemaps = distanceBetweenTilemaps;

        for (int line = 0; line < linesToGenerate; line++)
        {
            int numOfTilemapsToGenerate = Random.Range(minTilemapsPerLine, maxTilemapsPerLine + 1);
            int prefabIndex = Random.Range(0, platformTilemapPrefabs.Length);
            Tilemap prefabTilemap = platformTilemapPrefabs[prefabIndex];

            for (int i = 0; i < numOfTilemapsToGenerate; i++)
            {
                if (prefabTilemap.size.x > maxWidth)
                {
                    Debug.LogWarning($"Tilemap prefab {prefabTilemap.name} width exceeds maxWidth. Adjusting width.");
                    prefabTilemap.size = new Vector3Int(maxWidth, prefabTilemap.size.y, prefabTilemap.size.z);
                }

                int xOffset = i * (maxWidth + 1);
                PlaceTilemap(prefabTilemap, currentPosition + new Vector3Int(xOffset, 0, 0));
            }

            currentPosition -= new Vector3Int(0, prefabTilemap.size.y + (int)currentDistanceBetweenTilemaps, 0);

            // Reduce the distance between tilemaps for the next line
            currentDistanceBetweenTilemaps = Mathf.Max(0, currentDistanceBetweenTilemaps - distanceReductionPerLine);
        }
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