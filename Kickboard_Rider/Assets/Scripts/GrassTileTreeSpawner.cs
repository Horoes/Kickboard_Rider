using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileTreeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treePrefabs = new GameObject[3];
    [SerializeField]
    private GameObject[] animalPrefabs = new GameObject[3];


    public float animalSpeed = 5f; 
    public Vector3 animalScale = new Vector3((float)1.5, (float)1.5, (float)1.5);
    private float tileWidth = 69f;
    private float tileHeight = 3f;
    private int treeCount = 3; // 8��° ĭ���� 16��° ĭ�� �������� ������ ������ ����
    private GameObject selectedAnimalPrefab;
    private float spawnX;


    void Start()
    {
        selectedAnimalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
        spawnX = Random.value > 0.5f ? 0.45f : -0.45f;
        SpawnTrees();
        //SpawnAnimal();
    }

    void SpawnTrees()
    {
        // Ÿ���� ��ġ
        Vector3 tilePosition = transform.position;

        // Ÿ���� ���� x ��ġ
        float startX = tilePosition.x - tileWidth / 2 + tileHeight / 2;
        // Ÿ���� x�� ��ġ ���� ��
        int sections = (int)(tileWidth / tileHeight);

        // ������ ��ġ�� ���� ��ġ
        List<int> fixedPositions = new List<int> { 2, 3, 4, 5, 6, 16, 17, 18, 19, 20 };
        foreach (int section in fixedPositions)
        {
            // ���� ��ġ ��ġ ���
            float treeX = startX + section * tileHeight;

            // ������ ���� ������ ����
            GameObject selectedTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

            // ���� ���� �� ��ġ ����
            Vector3 treePosition = new Vector3(treeX, tilePosition.y, tilePosition.z); // y�� z���� Ÿ�� ��ġ ����
            GameObject treeInstance = Instantiate(selectedTreePrefab, treePosition, Quaternion.identity);

            // ������ Ÿ���� �ڽ����� ����
            treeInstance.transform.SetParent(transform);
        }

        // ���� ��ġ�� ���� ��ġ (8~16��° ĭ)
        List<int> usedPositions = new List<int>();
        while (usedPositions.Count < treeCount)
        {
            int randomSection = Random.Range(8, 17);
            if (!usedPositions.Contains(randomSection))
            {
                usedPositions.Add(randomSection);

                // ���� ��ġ ��ġ ���
                float treeX = startX + randomSection * tileHeight;

                // ������ ���� ������ ����
                GameObject selectedTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

                // ���� ���� �� ��ġ ����
                Vector3 treePosition = new Vector3(treeX, tilePosition.y, tilePosition.z); // y�� z���� Ÿ�� ��ġ ����
                GameObject treeInstance = Instantiate(selectedTreePrefab, treePosition, Quaternion.identity);

                // ������ Ÿ���� �ڽ����� ����
                treeInstance.transform.SetParent(transform);
            }
        }
    }

    void SpawnAnimal()
    {
        Vector3 spawnPosotion = new Vector3(spawnX, 0.5f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnX > 0 ? -90 : 90, 0);

        GameObject animalInstance = Instantiate(selectedAnimalPrefab, transform.TransformPoint(spawnPosotion), spawnRotation);

        animalInstance.transform.localScale = animalScale;


        StartCoroutine(MoveAnimal(animalInstance, spawnX));
    }

    IEnumerator MoveAnimal(GameObject animal, float direction)
    {
        Vector3 moveDirection = direction > 0 ? Vector3.left : Vector3.right;
        float elapsedTime = 0f;

        while (elapsedTime < 13f)
        {
            animal.transform.Translate(moveDirection * animalSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 5�� �� ���� �ı�
        Destroy(animal);
    }
}