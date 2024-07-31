using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;
    public float minimumLoadingTime = 2f; // 최소 로딩 시간 (초)
    public float loadingBarSpeed = 0.5f;  // 로딩 바 채워지는 속도

    private float targetProgress = 0f;    // 로딩 바 목표 진행도

    void Start()
    {
        // 새로운 씬 로드를 시작
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        // 로딩 시작 시간
        float startTime = Time.time;

        // 비동기 씬 로드를 시작
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;

        // 로딩이 완료될 때까지 대기
        while (!operation.isDone)
        {
            // 실제 로딩 진행 상황 계산
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // 로딩 바 목표 진행도 설정
            targetProgress = Mathf.Min(targetProgress + loadingBarSpeed * Time.deltaTime, progress);

            // 로딩 바 진행도 업데이트
            loadingBar.value = targetProgress;

            // 최소 로딩 시간이 지났는지 확인
            if (targetProgress >= 1f && Time.time >= startTime + minimumLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
