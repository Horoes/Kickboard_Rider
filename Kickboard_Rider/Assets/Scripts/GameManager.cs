using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    private int initialTileCount = 40;
    private Queue<GameObject> recentTiles;
    private int consecutiveRoadCount = 0;

    private void Awake()
    {
        // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫ º≥¡§
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        tilePrefabs = new List<GameObject>
        {
            grassTilePrefab,
            roadTilePrefab,
            sandTilePrefab
        };

        recentTiles = new Queue<GameObject>();

        for (int i = 0; i < initialTileCount; i++)
        {
            GenerateTile();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GenerateTile();
        }
    }

    void GenerateTile()
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

        recentTiles.Enqueue(selectedPrefab);

        if (recentTiles.Count > 2)
        {
            recentTiles.Dequeue();
        }
    }

    GameObject SelectTilePrefab()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 65f)
        {
            return roadTilePrefab;
        }
        else if (randomValue < 90f)
        {
            return grassTilePrefab;
        }
        else
        {
            return sandTilePrefab;
        }
    }
}
