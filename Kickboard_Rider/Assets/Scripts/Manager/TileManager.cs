using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }

    [SerializeField]
    private GameObject grassTilePrefab;
    [SerializeField]
    private GameObject roadTilePrefab;
    [SerializeField]
    private GameObject roadTile2Prefab;
    [SerializeField]
    private GameObject sandTilePrefab;

    private List<GameObject> tilePrefabs;
    private float zPosition = 0f;
    private int maxTileCount = 50;
    private Queue<GameObject> recentTiles = new Queue<GameObject>();
    private int consecutiveRoadCount = 0;
    private GameObject lastTilePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        tilePrefabs = new List<GameObject>
        {
            grassTilePrefab,
            roadTilePrefab,
            sandTilePrefab
        };

        for (int i = 0; i < 40; i++)
        {
            GenerateTile();
        }
    }

    public void GenerateTile()
    {
        GameObject selectedPrefab = SelectTilePrefab();

        if (selectedPrefab == roadTilePrefab || selectedPrefab == roadTile2Prefab)
        {
            consecutiveRoadCount++;
            if (consecutiveRoadCount > 1)
            {
                selectedPrefab = roadTile2Prefab;
            }
        }
        else
        {
            consecutiveRoadCount = 0;
        }

        GameObject newTile = Instantiate(selectedPrefab, new Vector3(0, 0, zPosition), Quaternion.identity);
        zPosition += 3f;

        recentTiles.Enqueue(newTile);

        if (recentTiles.Count > maxTileCount)
        {
            GameObject oldTile = recentTiles.Dequeue();
            Destroy(oldTile);
        }

        lastTilePrefab = selectedPrefab;
    }

    private GameObject SelectTilePrefab()
    {
        float randomValue = Random.Range(0f, 100f);
        GameObject selectedPrefab;

        if (randomValue < 65f)
        {
            selectedPrefab = roadTilePrefab;
        }
        else if (randomValue < 90f)
        {
            selectedPrefab = grassTilePrefab;
        }
        else
        {
            selectedPrefab = sandTilePrefab;
        }

        // Check if the last tile was a sand tile and select a different tile if so
        if (lastTilePrefab == sandTilePrefab)
        {
            while (selectedPrefab == sandTilePrefab)
            {
                randomValue = Random.Range(0f, 100f);

                if (randomValue < 65f)
                {
                    selectedPrefab = roadTilePrefab;
                }
                else if (randomValue < 90f)
                {
                    selectedPrefab = grassTilePrefab;
                }
                else
                {
                    selectedPrefab = sandTilePrefab;
                }
            }
        }

        return selectedPrefab;
    }
}
