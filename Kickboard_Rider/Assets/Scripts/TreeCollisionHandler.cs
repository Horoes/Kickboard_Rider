using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollisionHandler : MonoBehaviour
{
    private void Start()
    {
        // 모든 동물 오브젝트를 찾아 충돌을 무시하도록 설정합니다.
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals)
        {
            Collider treeCollider = GetComponent<Collider>();
            Collider animalCollider = animal.GetComponent<Collider>();

            if (treeCollider != null && animalCollider != null)
            {
                Physics.IgnoreCollision(treeCollider, animalCollider);
            }
        }
    }
}
