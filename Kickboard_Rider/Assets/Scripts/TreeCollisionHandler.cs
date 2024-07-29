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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if (player != null && !player.isHelmet)
            {
                Debug.Log("플레이어 충돌, isHelmet is false");
                // 추가로 실행할 코드 작성
                GameManager gameManager = GameManager.Instance;
                gameManager.PauseGame();
            }
            else if(player != null && player.isHelmet)
            {
                Debug.Log("플레이어 충돌, isHelmet is true");
                player.isHelmet = false;
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Tree collided with: " + collision.gameObject.name);
    }
}
