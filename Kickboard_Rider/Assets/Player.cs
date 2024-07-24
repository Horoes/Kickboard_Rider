using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 200.0f; // 이동 속도
    public float moveDistance = 3.0f; // 한 번에 이동할 거리
    public float minX = -12.0f; // X 좌표 최소값
    public float maxX = 12.0f; // X 좌표 최대값
    // private Animator animator; // 애니메이션 관련 변수
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool useRightFoot = true; 

    void Start()
    {
        // animator = GetComponent<Animator>(); 
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveCharacter();
        }
        else
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetTargetPosition(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetTargetPosition(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SetTargetPosition(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetTargetPosition(Vector3.right);
        }
    }

    void SetTargetPosition(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * moveDistance;

        // X 좌표가 범위 내에 있는지 확인
        if (newPosition.x < minX || newPosition.x > maxX)
        {
            return; // 범위를 벗어나면 이동하지 않음
        }

        // 목표 위치 설정
        targetPosition = newPosition;
        isMoving = true;

        // 애니메이션 전환
        // animator.SetBool("IsMoving", true);

        // if (useRightFoot)
        // {
        //     animator.SetTrigger("PushRight");
        // }
        // else
        // {
        //     animator.SetTrigger("PushLeft");
        // }

        // 다음 이동 시 반대 발 애니메이션 재생
        useRightFoot = !useRightFoot;
    }

    void MoveCharacter()
    {
        // 현재 위치와 목표 위치 사이를 부드럽게 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition; // 위치 보정
            isMoving = false;
            // animator.SetBool("IsMoving", false);
        }
    }
}