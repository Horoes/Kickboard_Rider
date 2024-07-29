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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if (player != null && !player.isHelmet)
            {
                Debug.Log("�÷��̾� �浹, isHelmet is false");
                // �߰��� ������ �ڵ� �ۼ�
                GameManager gameManager = GameManager.Instance;
                gameManager.PauseGame();
            }
            else if(player != null && player.isHelmet)
            {
                Debug.Log("�÷��̾� �浹, isHelmet is true");
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
