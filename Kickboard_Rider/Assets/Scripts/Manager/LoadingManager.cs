using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;
    public float minimumLoadingTime = 2f; // �ּ� �ε� �ð� (��)
    public float loadingBarSpeed = 0.5f;  // �ε� �� ä������ �ӵ�

    private float targetProgress = 0f;    // �ε� �� ��ǥ ���൵

    void Start()
    {
        // ���ο� �� �ε带 ����
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        // �ε� ���� �ð�
        float startTime = Time.time;

        // �񵿱� �� �ε带 ����
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;

        // �ε��� �Ϸ�� ������ ���
        while (!operation.isDone)
        {
            // ���� �ε� ���� ��Ȳ ���
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // �ε� �� ��ǥ ���൵ ����
            targetProgress = Mathf.Min(targetProgress + loadingBarSpeed * Time.deltaTime, progress);

            // �ε� �� ���൵ ������Ʈ
            loadingBar.value = targetProgress;

            // �ּ� �ε� �ð��� �������� Ȯ��
            if (targetProgress >= 1f && Time.time >= startTime + minimumLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
