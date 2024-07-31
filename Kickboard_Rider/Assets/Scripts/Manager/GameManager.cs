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
            DontDestroyOnLoad(gameObject); // GameManager�� ��� ������ ������
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� �ı�
            return;
        }
    }

    private void Start()
    {
        // ���� ȭ�� ��ȯ �� �Ŵ��� ����
        SetUpSceneManagers();
    }

    private void SetUpSceneManagers()
    {
        // ���� ȭ�鿡 ���� ���� UIManager�� TileManager ����
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
        return sceneName.Contains("Main"); // "Game"�̶�� �ܾ �� �̸��� ���ԵǾ� ������ true ��ȯ
    }

    private void OnEnable()
    {
        // �� ��ȯ �� �Ŵ��� �ν��Ͻ� ����
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� ��ȯ �� �Ŵ��� �ν��Ͻ� ���� ����
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // ���ο� ���� �ε�� �� �Ŵ��� �ν��Ͻ� ����
        SetUpSceneManagers();
    }
}
