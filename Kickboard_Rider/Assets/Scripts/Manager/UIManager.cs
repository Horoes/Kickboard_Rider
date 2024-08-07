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
    private bool isPaused = false; // 일시정지 상태를 나타내는 변수

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
        isPaused = true; // 일시정지 상태로 설정
        Debug.Log("Game paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false; // 일시정지 상태 해제
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
        // 플랫폼에 상관없이 뒤로가기 버튼 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameOver)
            {
                // 게임이 종료된 상태에서 뒤로가기 버튼을 눌렀을 때
                Debug.Log("게임 종료 - 뒤로가기 버튼");
                Application.Quit();
            }
            else if (isPaused)
            {
                // 게임이 일시정지 상태에서 뒤로가기 버튼을 눌렀을 때
                Debug.Log("게임 종료 - 일시정지 상태에서 뒤로가기 버튼");
                Application.Quit();
            }
            else
            {
                // 게임이 종료되지도, 일시정지도 않은 상태에서 뒤로가기 버튼을 눌렀을 때
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
