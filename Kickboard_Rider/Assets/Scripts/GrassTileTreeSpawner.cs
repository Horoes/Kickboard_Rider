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
        // 타일의 위치
        Vector3 tilePosition = transform.position;

        // 타일의 시작 x 위치
        float startX = tilePosition.x - tileWidth / 2 + tileHeight / 2;
        // 타일의 x축 배치 구역 수
        int sections = (int)(tileWidth / tileHeight);

        // 배치할 위치를 저장할 리스트
        System.Collections.Generic.List<int> usedPositions = new System.Collections.Generic.List<int>();

        for (int i = 0; i < treeCount; i++)
        {
            int randomSection;

            // 이미 사용된 위치를 피하여 랜덤 위치 선택
            do
            {
                randomSection = Random.Range(0, sections);
            } while (usedPositions.Contains(randomSection));

            usedPositions.Add(randomSection);

            // 나무 배치 위치 계산
            float treeX = startX + randomSection * tileHeight;

            // 랜덤한 나무 프리팹 선택
            GameObject selectedTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

            // 나무 생성 및 위치 설정
            Vector3 treePosition = new Vector3(treeX, tilePosition.y, tilePosition.z); // y와 z축은 타일 위치 기준
            GameObject treeInstance = Instantiate(selectedTreePrefab, treePosition, Quaternion.identity);

            // 나무를 타일의 자식으로 설정
            treeInstance.transform.SetParent(transform);
        }
    }
}

