using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject helmet; 
    private float tileWidth = 69f;
    private float tileHeight = 3f;
    private int treeCount = 1; // 8번째 칸부터 16번째 칸에 랜덤으로 생성할 나무의 개수
    private float spawnX;


    void Start()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < 20f)
            SpawnTrees();
    }

    void SpawnTrees()
    {
        // 타일의 위치
        Vector3 tilePosition = transform.position;

        // 타일의 시작 x 위치
        float startX = tilePosition.x - tileWidth / 2 + tileHeight / 2;
        // 타일의 x축 배치 구역 수
        int sections = (int)(tileWidth / tileHeight);


        // 랜덤 위치에 나무 배치 (8~16번째 칸)
        List<int> usedPositions = new List<int>();
        while (usedPositions.Count < treeCount)
        {
            int randomSection = Random.Range(8, 17);
            if (!usedPositions.Contains(randomSection))
            {
                usedPositions.Add(randomSection);

                // 나무 배치 위치 계산
                float treeX = startX + randomSection * tileHeight;

                // 나무 생성 및 위치 설정
                Vector3 treePosition = new Vector3(treeX, tilePosition.y, tilePosition.z); // y와 z축은 타일 위치 기준
                GameObject treeInstance = Instantiate(helmet, treePosition, Quaternion.identity);

                // 나무를 타일의 자식으로 설정
                treeInstance.transform.SetParent(transform);
            }
        }
    }
}
