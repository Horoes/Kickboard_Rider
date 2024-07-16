using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private void Start()
    {
        // ��� ���� ������Ʈ�� ã�� �浹�� �����ϵ��� �����մϴ�.
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject tree in trees)
        {
            Collider treeCollider = tree.GetComponent<Collider>();
            Collider animalCollider = GetComponent<Collider>();

            if (treeCollider != null && animalCollider != null)
            {
                Physics.IgnoreCollision(animalCollider, treeCollider);
            }
        }
    }
}
