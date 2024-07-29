using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 200.0f; // 이동 속도
    public float moveDistance = 3.0f; // 한 번에 이동할 거리
    public float minX = -12.0f; // X 좌표 최소값
    public float maxX = 12.0f; // X 좌표 최대값
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool useRightFoot = true;
    private Vector3 lastTilePosition; // 마지막 타일 위치
    public bool isHelmet = false;
    private float maxReachedZ = -6f; // 최고로 갔던 Z 좌표 초기값
    private float maxBackwardZ = -6f; // 뒤로 갈 수 있는 최대 Z 좌표 초기값

    void Start()
    {
        targetPosition = new Vector3(0, 1, -6); // 초기 위치 설정
        transform.position = targetPosition;
        lastTilePosition = targetPosition; // 초기 위치 저장
        Debug.Log("Starting position: " + lastTilePosition);
        Debug.Log("Initial maxReachedZ: " + maxReachedZ);
        Debug.Log("Initial maxBackwardZ: " + maxBackwardZ);
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

        if (Input.GetKeyDown(KeyCode.N))
        {
            isHelmet = true;
            Debug.Log("Helmet equipped");
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
            if (transform.position.z - moveDistance >= maxBackwardZ - (2 * moveDistance))
            {
                SetTargetPosition(Vector3.back);
                Debug.Log("Moved backward. Current Z: " + targetPosition.z);
            }
            else
            {
                Debug.Log("Cannot move backward. Out of bounds.");
            }
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

        if (newPosition.x < minX || newPosition.x > maxX)
        {
            Debug.Log("Cannot move. Position out of bounds.");
            return;
        }

        if (direction == Vector3.forward)
        {
            if (newPosition.z > lastTilePosition.z)
            {
                lastTilePosition = newPosition; // 마지막 타일 위치 갱신
                maxBackwardZ = newPosition.z; // 새로운 타일로 이동 시 뒤로 이동 가능 최대 Z 좌표 갱신
                GameManager.Instance.IncreaseScore(); // 점수 증가
                GameManager.Instance.GenerateTile(); // 타일 생성
                Debug.Log("Moved to a new forward tile. Current position: " + lastTilePosition);
                Debug.Log("Max backward Z reset to: " + maxBackwardZ);
            }
        }

        if (newPosition.z > maxReachedZ)
        {
            maxReachedZ = newPosition.z; // 새로운 최고 Z 좌표 갱신
            Debug.Log("New maxReachedZ: " + maxReachedZ);
        }

        targetPosition = newPosition;
        isMoving = true;
        useRightFoot = !useRightFoot;
    }

    void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
