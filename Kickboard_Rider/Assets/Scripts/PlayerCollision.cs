using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // private Animator animator;
    private bool isProtected = false;

    void Start()
    {
        // Rigidbody와 Collider 설정 확인
        //Rigidbody rb = GetComponent<Rigidbody>();
        //if (rb == null)
        //{
        //    rb = gameObject.AddComponent<Rigidbody>();
        //}
        //rb.isKinematic = false; // Rigidbody의 isKinematic 설정을 false로 변경

        //BoxCollider boxCollider = GetComponent<BoxCollider>();
        //if (boxCollider != null)
        //{
        //    boxCollider.isTrigger = true; // BoxCollider의 isTrigger 설정
        //}

        // animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (isProtected) return;

        if (other.CompareTag("Car") || other.CompareTag("Animal") || other.CompareTag("Tree"))
        {
            // 충돌한 객체 삭제
            Destroy(other.gameObject);

            // 로그 출력
            Debug.Log("Player collided with " + other.tag);

            // 게임 멈춤
            StartCoroutine(LogAndStopGame());
        }
    }

    public void SetProtection(bool protectionStatus)
    {
        isProtected = protectionStatus;
    }

    IEnumerator LogAndStopGame()
    {
        yield return new WaitForSeconds(1.5f); // 애니메이션 재생 시간 대기
        Debug.Log("Game paused.");
        // Time.timeScale = 0f;
    }
}
