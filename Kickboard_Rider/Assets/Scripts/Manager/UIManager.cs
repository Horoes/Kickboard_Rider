using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject GameoverUI;
    [SerializeField]
    private GameObject helmetUI;
    [SerializeField]
    private PlayerMovement player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI TotalText;
    private int score = 0;
    private bool isGameOver = false;

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

    public void UpdateScoreText()
    {
        if (scoreText != null && TotalText != null)
        {
            scoreText.text = score.ToString();
            TotalText.text = score.ToString();
        }
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game resumed.");
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        PauseGame();
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        ResumeGame();
    }

    public void GameOver()
    {
        PauseGame();
        isGameOver = true;
        GameoverUI.SetActive(true);
    }

    public void GoToMainMenu()
    {
        player.InitializePlayer();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void Update()
    {
        // Android 플랫폼에서 뒤로가기 버튼 감지
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // 일시정지 함수 호출
                Debug.Log("뒤로가기 버튼");
                Pause();
            }
        }

        if (isGameOver)
        {
            bool mouseClicked = Input.GetMouseButtonDown(0);
            bool touchDetected = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

            if (mouseClicked || touchDetected)
            {
                Time.timeScale = 1f;
                GoToMainMenu();
            }
        }

        if (player.isHelmet)
        {
            helmetUI.SetActive(true);
            Debug.Log("helmet active");
        }
        else
        {
            helmetUI.SetActive(false);
        }
    }
}
