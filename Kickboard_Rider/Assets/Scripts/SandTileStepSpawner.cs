using System.Collections.Generic;
using UnityEngine;

public class SandTileStepSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject stepPrefab;

    private float tileWidth = 69f;
    private float tileHeight = 3f;
    private int stepCount = 2; // ��ġ�� ������ ����
    private float spawnX;

    // ������ ��ġ�� ������ ��ġ
    private List<float> possiblePositions = new List<float> { 8.5f, 10.5f, 12.5f, 14.5f };

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

        // ���� ��ġ�� ���� ��ġ (������ ��ġ �߿���)
        List<int> usedPositions = new List<int>();
        while (usedPositions.Count < stepCount)
        {
            int randomIndex = Random.Range(0, possiblePositions.Count);
            if (!usedPositions.Contains(randomIndex))
            {
                usedPositions.Add(randomIndex);

                // ���� ��ġ ��ġ ���
                float treeX = startX + possiblePositions[randomIndex] * tileHeight;

                // ���� ���� �� ��ġ ����
                Vector3 treePosition = new Vector3(treeX, tilePosition.y + 0.1f , tilePosition.z); // y�� z���� Ÿ�� ��ġ ����
                GameObject treeInstance = Instantiate(stepPrefab, treePosition, Quaternion.identity);

                // ������ Ÿ���� �ڽ����� ����
                treeInstance.transform.SetParent(transform);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.isSand = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.isSand = false;
        }
    }
}
