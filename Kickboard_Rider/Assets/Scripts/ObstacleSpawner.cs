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
        // ù ��° ���� �� ������ �� ������ ����
        selectedPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // 2�ʿ��� 4�� ������ ���� ���� �ֱ� ����
        spawnInterval = Random.Range(2f, 4f);
        obstacleSpeed = Random.Range(5f,10f);

        switch (tileType)
        {
            case TileType.Forest:
                spawnInterval = Random.Range(10f, 15f);  // Full Ÿ�Ͽ����� �ֱ⸦ ���
                obstacleSpeed = Random.Range(10f, 20f);  // �ӵ��� ����
                break;
            case TileType.Road:
            default:
                spawnInterval = Random.Range(2f, 4f);
                obstacleSpeed = Random.Range(5f, 10f);
                break;
        }

        // x ��ǥ ������ �����ϰ� �� �� ����
        spawnX = Random.value > 0.5f ? 0.45f : -0.45f;

        // ó�� ������ 0~2�� ������ ���� �ð� �Ŀ� ����
        float initialDelay = Random.Range(0f, 4f);
        StartCoroutine(StartSpawning(initialDelay));
    }

    void OnDestroy()
    {
        // RoadTile�� �ı��� �� �ڷ�ƾ�� ������ŵ�ϴ�.
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
            yield return new WaitForSeconds(spawnInterval); // ���� ���� �ֱ� ���
        }
    }

    void SpawnObstacle()
    {
        
        Vector3 spawnPosition = new Vector3(spawnX, 1.5f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnX > 0 ? -90 : 90, 0); // -90�� �Ǵ� 90�� ȸ���� ���·� ����

        GameObject carInstance = Instantiate(selectedPrefab, transform.TransformPoint(spawnPosition), spawnRotation);


        // Obstacle �̵�
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

        // 5�� �� ���� �ı�
        Destroy(obstacle);
    }
}
