using System.Collections.Generic;
using UnityEngine;

public class SandTileStepSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject stepPrefab;

    private float tileWidth = 69f;
    private float tileHeight = 3f;
    private int stepCount = 2; // 배치할 나무의 개수
    private float spawnX;

    // 나무를 배치할 가능한 위치
    private List<float> possiblePositions = new List<float> { 8.5f, 10.5f, 12.5f, 14.5f };

    void Start()
    {
        SpawnTrees();
    }

    void SpawnTrees()
    {
        // 타일의 위치
        Vector3 tilePosition = transform.position;

        // 타일의 시작 x 위치
        float startX = tilePosition.x - tileWidth / 2 + tileHeight / 2;

        // 랜덤 위치에 나무 배치 (가능한 위치 중에서)
        List<int> usedPositions = new List<int>();
        while (usedPositions.Count < stepCount)
        {
            int randomIndex = Random.Range(0, possiblePositions.Count);
            if (!usedPositions.Contains(randomIndex))
            {
                usedPositions.Add(randomIndex);

                // 나무 배치 위치 계산
                float treeX = startX + possiblePositions[randomIndex] * tileHeight;

                // 나무 생성 및 위치 설정
                Vector3 treePosition = new Vector3(treeX, tilePosition.y + 0.1f , tilePosition.z); // y와 z축은 타일 위치 기준
                GameObject treeInstance = Instantiate(stepPrefab, treePosition, Quaternion.identity);

                // 나무를 타일의 자식으로 설정
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
