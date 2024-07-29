using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ�� Transform
    public Vector3 offset = new Vector3(0, 5, -10); // �⺻ ������
    public float heightOffset = 5.0f; // ī�޶� ������ ����
    public Vector3 rotation = new Vector3(10, 0, 0); // ī�޶��� ȸ��

    void Start()
    {
        // �ʱ� ��ġ�� ȸ���� ����
        if (player != null)
        {
            SetCameraPositionAndRotation();
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // �� �����Ӹ��� ��ġ�� ȸ���� ������Ʈ
            SetCameraPositionAndRotation();
        }
    }

    void SetCameraPositionAndRotation()
    {
        // ������ ������ ������ ī�޶� ��ġ ����
        Vector3 newOffset = offset;
        newOffset.y = heightOffset;
        transform.position = player.position + newOffset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
