using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratePlatform : MonoBehaviour
{
    public Tilemap platformTilemap;
    public Tilemap[] platformTilemapPrefabs;

    public int minTilemapsPerLine = 3;
    public int maxTilemapsPerLine = 3;
    public float initialDistanceBetweenTilemaps = 1f; // Initial distance between tilemaps
    public float minDistanceBetweenTilemaps = 0.2f; // Minimum distance between tilemaps
    public float distanceReductionPerLine = 0.1f;
    public int maxWidth = 7;
    public Transform player;
    public float scrollSpeed = 2f;

    private Vector3 initialPlayerPosition;
    private Vector3Int lastGeneratedPosition;
    private float totalDistanceGenerated;
    private float distanceBetweenTilemaps;

    private bool hasGeneratedInitialPlatforms = false;
    private bool generateNextLines = true;
    private int linesToGenerate = 2; // Number of lines to generate below the last line
    [SerializeField] private float spawnInterval = 2f;
    private float spawnTimer;

    void Start()
    {
        initialPlayerPosition = player.position;
        lastGeneratedPosition = platformTilemap.origin - new Vector3Int(0, 1, 0);
        distanceBetweenTilemaps = initialDistanceBetweenTilemaps;
    }

    void Update()
    {
        //if (player.position.y < initialPlayerPosition.y)
        //{
            Vector3 move = new Vector3(0, scrollSpeed * Time.deltaTime, 0);
            platformTilemap.transform.position += move;

            float distanceTraveled = initialPlayerPosition.y - player.position.y;

            // Debug.Log statements to understand generation conditions
            Debug.Log($"Distance Traveled: {distanceTraveled}, Total Distance Generated: {totalDistanceGenerated}, Distance Between Tilemaps: {distanceBetweenTilemaps}");

        // Check if we need to generate more platforms
        //if (generateNextLines && distanceTraveled > totalDistanceGenerated + distanceBetweenTilemaps)
        //{
        //    for (int line = 0; line < linesToGenerate; line++)
        //    {
        //        GeneratePlatforms();
        //        totalDistanceGenerated += distanceBetweenTilemaps;
        //    }
        //    generateNextLines = false; // Set to false until player moves down again
        //}
        spawnTimer += Time.deltaTime;
        if(PointSystem.instance.currentPoint == 10)
        {
            scrollSpeed = 4;
        }
        if (PointSystem.instance.currentPoint == 20)
        {
            scrollSpeed = 5;
        }
        if (PointSystem.instance.currentPoint == 30)
        {
            scrollSpeed = 6;
        }
        if (spawnTimer >= spawnInterval)
        {
            for (int line = 0; line < linesToGenerate; line++)
            {

                GeneratePlatforms();
                totalDistanceGenerated += distanceBetweenTilemaps;
            }
            spawnTimer = 0f;
        }



        RemovePlatformsAboveCamera();

            // Check if player is close to last line to generate next lines
            Vector3Int playerCellPosition = platformTilemap.WorldToCell(player.position);
            Vector3Int lastLinePosition = platformTilemap.origin - new Vector3Int(0, linesToGenerate * platformTilemapPrefabs[0].size.y, 0);

            // Adjust the tolerance value (0.1f) based on your platform size and player movement speed
            if (playerCellPosition.y <= lastLinePosition.y + 0.1f)
            {
                generateNextLines = true;
            }
        //}
        //else
        //{
        //    // If player jumps back up or hasn't moved down enough, reset generation flags
        //    totalDistanceGenerated = 0f;
        //    generateNextLines = true;
        //}
    }

    void GeneratePlatforms()
    {
        Vector3Int currentPosition = lastGeneratedPosition;

        int numOfTilemapsToGenerate = Random.Range(minTilemapsPerLine, maxTilemapsPerLine + 1);

        for (int i = 0; i < numOfTilemapsToGenerate; i++)
        {
            int prefabIndex = Random.Range(0, platformTilemapPrefabs.Length);
            Tilemap prefabTilemap = platformTilemapPrefabs[prefabIndex];

            if (prefabTilemap.size.x > maxWidth)
            {
                Debug.LogWarning($"Tilemap prefab {prefabTilemap.name} width exceeds maxWidth. Adjusting width.");
                prefabTilemap.size = new Vector3Int(maxWidth, prefabTilemap.size.y, prefabTilemap.size.z);
            }

            int startX = currentPosition.x + (platformTilemap.size.x - prefabTilemap.size.x) / 2;
            int xOffset = i * (maxWidth + 1);
            Vector3Int platformPosition = new Vector3Int(startX + xOffset, currentPosition.y, currentPosition.z);

            platformPosition.x = Mathf.RoundToInt(platformPosition.x);

            PlaceTilemap(prefabTilemap, platformPosition);
        }

        // Adjust position for next line of platforms
        lastGeneratedPosition -= new Vector3Int(0, platformTilemapPrefabs[0].size.y + Mathf.RoundToInt(distanceBetweenTilemaps), 0);

        // Reduce distance between platforms for next line
        distanceBetweenTilemaps = Mathf.Max(minDistanceBetweenTilemaps, distanceBetweenTilemaps - distanceReductionPerLine);

        hasGeneratedInitialPlatforms = true; // Mark that initial platforms have been generated
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

    void RemovePlatformsAboveCamera()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3Int cameraPositionInt = new Vector3Int(Mathf.RoundToInt(cameraPosition.x), Mathf.RoundToInt(cameraPosition.y), Mathf.RoundToInt(cameraPosition.z));

        BoundsInt bounds = platformTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Vector3 worldPosition = platformTilemap.CellToWorld(tilePosition);

                if (worldPosition.y > cameraPosition.y + Camera.main.orthographicSize)
                {
                    platformTilemap.SetTile(tilePosition, null);
                }
            }
        }
    }
}