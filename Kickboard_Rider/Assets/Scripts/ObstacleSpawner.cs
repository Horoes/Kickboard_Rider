using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public enum TileType { Road, Forest }

    [SerializeField]
    public TileType tileType;

    [SerializeField]
    private GameObject[] obstaclePrefabs;
    [SerializeField]
    private float obstacleSpeed = 5f;


    private Coroutine spawnCoroutine;
    private GameObject selectedPrefab;
    private float spawnInterval;
    private float spawnX;
    void Start()
    {
        // 첫 번째 스폰 시 랜덤한 차 프리팹 선택
        selectedPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // 2초에서 4초 사이의 랜덤 생성 주기 설정
        spawnInterval = Random.Range(2f, 4f);
        obstacleSpeed = Random.Range(5f,10f);

        switch (tileType)
        {
            case TileType.Forest:
                spawnInterval = Random.Range(10f, 15f);  // Full 타일에서는 주기를 길게
                obstacleSpeed = Random.Range(10f, 20f);  // 속도도 높게
                break;
            case TileType.Road:
            default:
                spawnInterval = Random.Range(2f, 4f);
                obstacleSpeed = Random.Range(5f, 10f);
                break;
        }

        // x 좌표 방향을 랜덤하게 한 번 설정
        spawnX = Random.value > 0.5f ? 0.45f : -0.45f;

        // 처음 스폰을 0~2초 사이의 랜덤 시간 후에 시작
        float initialDelay = Random.Range(0f, 4f);
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
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval); // 랜덤 생성 주기 사용
        }
    }

    void SpawnObstacle()
    {
        
        Vector3 spawnPosition = new Vector3(spawnX, 1.5f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnX > 0 ? -90 : 90, 0); // -90도 또는 90도 회전한 상태로 스폰

        GameObject carInstance = Instantiate(selectedPrefab, transform.TransformPoint(spawnPosition), spawnRotation);


        // Obstacle 이동
        StartCoroutine(MoveCar(carInstance, spawnX, obstacleSpeed));
    }

    IEnumerator MoveCar(GameObject obstacle, float direction, float speed)
    {
        Vector3 moveDirection = direction > 0 ? Vector3.left : Vector3.right;
        float elapsedTime = 0f;
        float destroyTime = 65 / speed;
        while (elapsedTime < destroyTime)
        {
            obstacle.transform.Translate(moveDirection * obstacleSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 5초 후 차를 파괴
        Destroy(obstacle);
    }
}
