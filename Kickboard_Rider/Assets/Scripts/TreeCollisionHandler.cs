using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollisionHandler : MonoBehaviour
{
    private void Start()
    {
        // ��� ���� ������Ʈ�� ã�� �浹�� �����ϵ��� �����մϴ�.
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
