using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 200.0f; // �̵� �ӵ�
    public float moveDistance = 3.0f; // �� ���� �̵��� �Ÿ�
    public float minX = -12.0f; // X ��ǥ �ּҰ�
    public float maxX = 12.0f; // X ��ǥ �ִ밪
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool useRightFoot = true;
    private Vector3 lastTilePosition; // ������ Ÿ�� ��ġ
    public bool isHelmet = false;
    private float maxReachedZ = -6f; // �ְ�� ���� Z ��ǥ �ʱⰪ
    private float maxBackwardZ = -6f; // �ڷ� �� �� �ִ� �ִ� Z ��ǥ �ʱⰪ

    void Start()
    {
        targetPosition = new Vector3(0, 1, -6); // �ʱ� ��ġ ����
        transform.position = targetPosition;
        lastTilePosition = targetPosition; // �ʱ� ��ġ ����
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
                lastTilePosition = newPosition; // ������ Ÿ�� ��ġ ����
                maxBackwardZ = newPosition.z; // ���ο� Ÿ�Ϸ� �̵� �� �ڷ� �̵� ���� �ִ� Z ��ǥ ����
                GameManager.Instance.IncreaseScore(); // ���� ����
                GameManager.Instance.GenerateTile(); // Ÿ�� ����
                Debug.Log("Moved to a new forward tile. Current position: " + lastTilePosition);
                Debug.Log("Max backward Z reset to: " + maxBackwardZ);
            }
        }

        if (newPosition.z > maxReachedZ)
        {
            maxReachedZ = newPosition.z; // ���ο� �ְ� Z ��ǥ ����
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
