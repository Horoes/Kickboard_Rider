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
    private bool isPaused = false; // �Ͻ����� ���¸� ��Ÿ���� ����
    public AudioClip clickSound; // Ŭ�� ����
    private AudioSource audioSource;

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
    void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
        isPaused = true; // �Ͻ����� ���·� ����
        Debug.Log("Game paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false; // �Ͻ����� ���� ����
        Debug.Log("Game resumed.");
    }

    public void Pause()
    {
        audioSource.PlayOneShot(clickSound);
        pauseUI.SetActive(true);
        PauseGame();
    }

    public void Resume()
    {
        audioSource.PlayOneShot(clickSound);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameOver)
            {
                Debug.Log("���� ���� - �ڷΰ��� ��ư");
                Application.Quit();
            }
            else if (isPaused)
            {
                Debug.Log("���� ���� - �Ͻ����� ���¿��� �ڷΰ��� ��ư");
                Application.Quit();
            }
            else
            {
                Debug.Log("�ڷΰ��� ��ư");
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
