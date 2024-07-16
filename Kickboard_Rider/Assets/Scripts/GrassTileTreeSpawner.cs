using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileTreeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treePrefabs = new GameObject[3];
    [SerializeField]
    private GameObject[] animalPrefabs = new GameObject[6];

    public Vector3 animalScale = new Vector3((float)1.5, (float)1.5, (float)1.5);
    private float tileWidth = 69f;
    private float tileHeight = 3f;
    private int treeCount = 3; // 8��° ĭ���� 16��° ĭ�� �������� ������ ������ ����

    void Start()
    {
        SpawnTrees();
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
}