using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UIManager uiManager { get; private set; }
    public TileManager tileManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // GameManager는 모든 씬에서 유지됨
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
            return;
        }
    }

    private void Start()
    {
        // 게임 화면 전환 시 매니저 설정
        SetUpSceneManagers();
    }

    private void SetUpSceneManagers()
    {
        // 게임 화면에 있을 때만 UIManager와 TileManager 설정
        if (IsGameScene())
        {
            uiManager = FindObjectOfType<UIManager>();
            tileManager = FindObjectOfType<TileManager>();

            if (uiManager == null)
                Debug.LogWarning("UIManager is not available in the scene.");

            if (tileManager == null)
                Debug.LogWarning("TileManager is not available in the scene.");
        }
    }

    private bool IsGameScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return sceneName.Contains("Main"); // "Game"이라는 단어가 씬 이름에 포함되어 있으면 true 반환
    }

    private void OnEnable()
    {
        // 씬 전환 시 매니저 인스턴스 설정
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 전환 시 매니저 인스턴스 설정 해제
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // 새로운 씬이 로드될 때 매니저 인스턴스 설정
        SetUpSceneManagers();
    }
}
