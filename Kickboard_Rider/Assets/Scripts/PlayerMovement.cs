using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // 기존 필드
    private const float playerY = 0.3f;
    public float speed = 200.0f;
    public float moveDistance = 3.0f;
    public float minX = -12.0f;
    public float maxX = 12.0f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool useRightFoot = true;
    private Vector3 lastTilePosition;
    public bool isHelmet = false;
    private float maxReachedZ = -6f;
    private float maxBackwardZ = -6f;
    public bool isStep = false;
    public bool isSand = false;
    private Rigidbody rb;
    // 스와이프 필드
    private Vector2 swipeStart;
    private Vector2 swipeEnd;
    private bool isSwiping = false;
    private float minSwipeDistance = 50f; // 스와이프 최소 거리

    // 게임오버 높이 설정
    private float gameOverHeight = -10f;

    void Start()
    {
        InitializePlayer();
    }

    void Update()
    {
        if (isStep == false && isSand == true)
        {
            ActivateGravity();
        }

        if (rb.useGravity)
        {
            CheckGameOver();
        }
        else
        {
            if (isMoving)
            {
                MoveCharacter();
            }
            else
            {
                HandleInput();
                HandleSwipe();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            isHelmet = true;
            Debug.Log("Helmet equipped");
        }
    }

    void ActivateGravity()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        isMoving = false; // 이동 중지
    }

    void CheckGameOver()
    {
        if (transform.position.y < gameOverHeight)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // 게임오버 처리 로직
        Debug.Log("Game Over!");
        GameManager.Instance.uiManager.GameOver();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetTargetPosition(Vector3.forward);
            LookInDirection(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.position.z - moveDistance >= maxBackwardZ - (2 * moveDistance))
            {
                SetTargetPosition(Vector3.back);
                LookInDirection(Vector3.back);
            }
            else
            {
                Debug.Log("Cannot move backward. Out of bounds.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SetTargetPosition(Vector3.left);
            LookInDirection(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetTargetPosition(Vector3.right);
            LookInDirection(Vector3.right);
        }
    }

    void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                swipeEnd = touch.position;
                if (Vector2.Distance(swipeStart, swipeEnd) >= minSwipeDistance)
                {
                    DetectSwipeDirection();
                    isSwiping = false;
                }
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                swipeEnd = touch.position;
                DetectSwipeDirection();
                isSwiping = false;
            }
        }
    }

    void DetectSwipeDirection()
    {
        Vector2 swipeDelta = swipeEnd - swipeStart;

        if (swipeDelta.magnitude < minSwipeDistance)
        {
            // 스와이프 거리가 너무 짧으면 무시
            return;
        }

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0)
            {
                SetTargetPosition(Vector3.right);
                LookInDirection(Vector3.right);
            }
            else
            {
                SetTargetPosition(Vector3.left);
                LookInDirection(Vector3.left);
            }
        }
        else
        {
            if (swipeDelta.y > 0)
            {
                SetTargetPosition(Vector3.forward);
                LookInDirection(Vector3.forward);
            }
            else
            {
                if (transform.position.z - moveDistance >= maxBackwardZ - (2 * moveDistance))
                {
                    SetTargetPosition(Vector3.back);
                    LookInDirection(Vector3.back);
                }
            }
        }
    }

    void LookInDirection(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = newRotation;
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
                lastTilePosition = newPosition;
                maxBackwardZ = newPosition.z;
                GameManager.Instance.uiManager.IncreaseScore();
                GameManager.Instance.tileManager.GenerateTile();
                Debug.Log("Moved to a new forward tile. Current position: " + lastTilePosition);
                Debug.Log("Max backward Z reset to: " + maxBackwardZ);
            }
        }

        if (newPosition.z > maxReachedZ)
        {
            maxReachedZ = newPosition.z;
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

    public void InitializePlayer()
    {
        targetPosition = new Vector3(0, playerY, -6);
        transform.position = targetPosition;
        lastTilePosition = targetPosition;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        isMoving = false;
        isStep = false;
        isSand = false;
    }
}
