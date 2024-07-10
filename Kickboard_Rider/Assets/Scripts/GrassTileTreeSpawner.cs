using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileTreeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treePrefabs = new GameObject[3];
    private float tileWidth = 24f;
    private float tileHeight = 3f;
    private int treeCount = 3;

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

        // ��ġ�� ��ġ�� ������ ����Ʈ
        System.Collections.Generic.List<int> usedPositions = new System.Collections.Generic.List<int>();

        for (int i = 0; i < treeCount; i++)
        {
            int randomSection;

            // �̹� ���� ��ġ�� ���Ͽ� ���� ��ġ ����
            do
            {
                randomSection = Random.Range(0, sections);
            } while (usedPositions.Contains(randomSection));

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

