using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject helmet; 
    private float tileWidth = 69f;
    private float tileHeight = 3f;
    private int treeCount = 1; // 8��° ĭ���� 16��° ĭ�� �������� ������ ������ ����
    private float spawnX;


    void Start()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < 20f)
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

                // ���� ���� �� ��ġ ����
                Vector3 treePosition = new Vector3(treeX, tilePosition.y, tilePosition.z); // y�� z���� Ÿ�� ��ġ ����
                GameObject treeInstance = Instantiate(helmet, treePosition, Quaternion.identity);

                // ������ Ÿ���� �ڽ����� ����
                treeInstance.transform.SetParent(transform);
            }
        }
    }
}
