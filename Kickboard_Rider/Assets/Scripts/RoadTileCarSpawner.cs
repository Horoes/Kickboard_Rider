using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTileCarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] carPrefabs = new GameObject[6];
    public float carSpeed = 5f; // 차 이동 속도
    public Vector3 carScale = new Vector3(1, 1, 1); // 자동차의 원하는 크기 설정

    private Coroutine spawnCoroutine;
    private GameObject selectedCarPrefab;
    private float spawnInterval;
    private float spawnX;

    void Start()
    {
        // 첫 번째 스폰 시 랜덤한 차 프리팹 선택
        selectedCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // 2초에서 4초 사이의 랜덤 생성 주기 설정
        spawnInterval = Random.Range(2f, 4f);

        // x 좌표 방향을 랜덤하게 한 번 설정
        spawnX = Random.value > 0.5f ? 0.45f : -0.45f;

        // 처음 스폰을 0~2초 사이의 랜덤 시간 후에 시작
        float initialDelay = Random.Range(0f, 2f);
        StartCoroutine(StartSpawning(initialDelay));
    }

    void OnDestroy()
    {
        // RoadTile이 파괴될 때 코루틴을 중지시킵니다.
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator StartSpawning(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnCoroutine = StartCoroutine(SpawnCarsPeriodically());
    }

    IEnumerator SpawnCarsPeriodically()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(spawnInterval); // 랜덤 생성 주기 사용
        }
    }

    void SpawnCar()
    {
        // 도로 타일의 로컬 좌표를 기준으로 상대 위치에 차 스폰
        Vector3 spawnPosition = new Vector3(spawnX, 1.5f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnX > 0 ? -90 : 90, 0); // -90도 또는 90도 회전한 상태로 스폰

        GameObject carInstance = Instantiate(selectedCarPrefab, transform.TransformPoint(spawnPosition), spawnRotation);

        // 스폰된 자동차의 스케일을 원하는 크기로 설정
        carInstance.transform.localScale = carScale;

        // Rigidbody 컴포넌트 설정
        Rigidbody rb = carInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("car spawn");
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        // 차 이동
        StartCoroutine(MoveCar(carInstance, spawnX));
    }

    IEnumerator MoveCar(GameObject car, float direction)
    {
        Vector3 moveDirection = direction > 0 ? Vector3.left : Vector3.right;
        float elapsedTime = 0f;

        while (elapsedTime < 13f)
        {
            car.transform.Translate(moveDirection * carSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 5초 후 차를 파괴
        Destroy(car);
    }
}
