using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratePlatform : MonoBehaviour
{
    public Tilemap platformTilemap;
    public Tilemap[] platformTilemapPrefabs; // Array of Tilemap prefabs

    public int linesToGenerate = 3; // Number of lines of tilemaps to generate
    public int minTilemapsPerLine = 1; // Minimum number of tilemaps per line
    public int maxTilemapsPerLine = 3; // Maximum number of tilemaps per line
    public float distanceBetweenTilemaps = 1f; // Vertical distance between each generated Tilemap
    public int maxWidth = 5; // Maximum width of generated Tilemaps

    void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        // Get the bottom-left corner of the platformTilemap
        Vector3Int platformPosition = platformTilemap.origin;

        // Start generating from just below the platformTilemap
        Vector3Int currentPosition = platformPosition - new Vector3Int(0, 1, 0); // Adjust based on your Tilemap's cell size and layout

        for (int line = 0; line < linesToGenerate; line++)
        {
            int numOfTilemapsToGenerate = Random.Range(minTilemapsPerLine, maxTilemapsPerLine + 1); // Generate between min and max Tilemaps per line
            int prefabIndex = Random.Range(0, platformTilemapPrefabs.Length);
            Tilemap prefabTilemap = platformTilemapPrefabs[prefabIndex];

            for (int i = 0; i < numOfTilemapsToGenerate; i++)
            {
                // Adjust width if it exceeds maxWidth
                if (prefabTilemap.size.x > maxWidth)
                {
                    Debug.LogWarning($"Tilemap prefab {prefabTilemap.name} width exceeds maxWidth. Adjusting width.");
                    prefabTilemap.size = new Vector3Int(maxWidth, prefabTilemap.size.y, prefabTilemap.size.z);
                }

                // Calculate horizontal offset to place Tilemap in a line under platformTilemap
                int xOffset = i * (maxWidth + 1); // Adjust spacing as needed

                // Place prefab Tilemap at the current position
                PlaceTilemap(prefabTilemap, currentPosition + new Vector3Int(xOffset, 0, 0));
            }

            // Update current position for the next line of Tilemaps (move downward)
            currentPosition -= new Vector3Int(0, prefabTilemap.size.y + (int)distanceBetweenTilemaps, 0); // Adjust spacing as needed
        }
    }

    void PlaceTilemap(Tilemap tilemapPrefab, Vector3Int position)
    {
        // Iterate through all cells in the prefab Tilemap and place them onto the main platformTilemap
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
