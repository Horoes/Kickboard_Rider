using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // private Animator animator;
    private bool isProtected = false;

    void Start()
    {
        // Rigidbody�� Collider ���� Ȯ��
        //Rigidbody rb = GetComponent<Rigidbody>();
        //if (rb == null)
        //{
        //    rb = gameObject.AddComponent<Rigidbody>();
        //}
        //rb.isKinematic = false; // Rigidbody�� isKinematic ������ false�� ����

        //BoxCollider boxCollider = GetComponent<BoxCollider>();
        //if (boxCollider != null)
        //{
        //    boxCollider.isTrigger = true; // BoxCollider�� isTrigger ����
        //}

        // animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (isProtected) return;

        if (other.CompareTag("Car") || other.CompareTag("Animal") || other.CompareTag("Tree"))
        {
            // �浹�� ��ü ����
            Destroy(other.gameObject);

            // �α� ���
            Debug.Log("Player collided with " + other.tag);

            // ���� ����
            StartCoroutine(LogAndStopGame());
        }
    }

    public void SetProtection(bool protectionStatus)
    {
        isProtected = protectionStatus;
    }

    IEnumerator LogAndStopGame()
    {
        yield return new WaitForSeconds(1.5f); // �ִϸ��̼� ��� �ð� ���
        Debug.Log("Game paused.");
        // Time.timeScale = 0f;
    }
}
