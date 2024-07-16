using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTileCarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] carPrefabs = new GameObject[6];
    public float carSpeed = 5f; // �� �̵� �ӵ�
    public Vector3 carScale = new Vector3(1, 1, 1); // �ڵ����� ���ϴ� ũ�� ����

    private Coroutine spawnCoroutine;
    private GameObject selectedCarPrefab;
    private float spawnInterval;
    private float spawnX;

    void Start()
    {
        // ù ��° ���� �� ������ �� ������ ����
        selectedCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // 2�ʿ��� 4�� ������ ���� ���� �ֱ� ����
        spawnInterval = Random.Range(2f, 4f);

        // x ��ǥ ������ �����ϰ� �� �� ����
        spawnX = Random.value > 0.5f ? 0.45f : -0.45f;

        // ó�� ������ 0~2�� ������ ���� �ð� �Ŀ� ����
        float initialDelay = Random.Range(0f, 2f);
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
            SpawnCar();
            yield return new WaitForSeconds(spawnInterval); // ���� ���� �ֱ� ���
        }
    }

    void SpawnCar()
    {
        // ���� Ÿ���� ���� ��ǥ�� �������� ��� ��ġ�� �� ����
        Vector3 spawnPosition = new Vector3(spawnX, 1.5f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnX > 0 ? -90 : 90, 0); // -90�� �Ǵ� 90�� ȸ���� ���·� ����

        GameObject carInstance = Instantiate(selectedCarPrefab, transform.TransformPoint(spawnPosition), spawnRotation);

        // ������ �ڵ����� �������� ���ϴ� ũ��� ����
        carInstance.transform.localScale = carScale;

        // Rigidbody ������Ʈ ����
        Rigidbody rb = carInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("car spawn");
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        // �� �̵�
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

        // 5�� �� ���� �ı�
        Destroy(car);
    }
}
