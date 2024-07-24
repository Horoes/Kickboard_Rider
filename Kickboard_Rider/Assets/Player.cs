using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 200.0f; // �̵� �ӵ�
    public float moveDistance = 3.0f; // �� ���� �̵��� �Ÿ�
    public float minX = -12.0f; // X ��ǥ �ּҰ�
    public float maxX = 12.0f; // X ��ǥ �ִ밪
    // private Animator animator; // �ִϸ��̼� ���� ����
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

        // X ��ǥ�� ���� ���� �ִ��� Ȯ��
        if (newPosition.x < minX || newPosition.x > maxX)
        {
            return; // ������ ����� �̵����� ����
        }

        // ��ǥ ��ġ ����
        targetPosition = newPosition;
        isMoving = true;

        // �ִϸ��̼� ��ȯ
        // animator.SetBool("IsMoving", true);

        // if (useRightFoot)
        // {
        //     animator.SetTrigger("PushRight");
        // }
        // else
        // {
        //     animator.SetTrigger("PushLeft");
        // }

        // ���� �̵� �� �ݴ� �� �ִϸ��̼� ���
        useRightFoot = !useRightFoot;
    }

    void MoveCharacter()
    {
        // ���� ��ġ�� ��ǥ ��ġ ���̸� �ε巴�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ǥ ��ġ�� �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition; // ��ġ ����
            isMoving = false;
            // animator.SetBool("IsMoving", false);
        }
    }
}