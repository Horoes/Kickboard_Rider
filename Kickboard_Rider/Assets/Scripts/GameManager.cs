using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private int initialTileCount = 40; // 초기 타일 수
    private int maxTileCount = 50; // 최대 타일 수
    private Queue<GameObject> recentTiles = new Queue<GameObject>();
    private int consecutiveRoadCount = 0;
    private int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
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

        for (int i = 0; i < initialTileCount; i++)
        {
            GenerateTile();
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
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

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
        Debug.Log("Score: " + score);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game paused.");
    }

    public void ResumeGame()
    {
        Debug.Log("Game resumed.");
    }
}
