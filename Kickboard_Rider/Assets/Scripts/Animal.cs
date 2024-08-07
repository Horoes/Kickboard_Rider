using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private void Start()
    {
        // 모든 나무 오브젝트를 찾아 충돌을 무시하도록 설정합니다.
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if (player != null && !player.isHelmet)
            {
                Debug.Log("플레이어 충돌, isHelmet is false");
                GameManager.Instance.uiManager.GameOver();

            }
            else if (player != null && player.isHelmet)
            {
                Debug.Log("플레이어 충돌, isHelmet is true");
                player.isHelmet = false;
            }
        }

        Destroy(gameObject);
    }
}
