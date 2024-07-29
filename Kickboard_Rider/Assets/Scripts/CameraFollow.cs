using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform
    public Vector3 offset = new Vector3(0, 5, -10); // 기본 오프셋
    public float heightOffset = 5.0f; // 카메라 높낮이 조절
    public Vector3 rotation = new Vector3(10, 0, 0); // 카메라의 회전

    void Start()
    {
        // 초기 위치와 회전을 설정
        if (player != null)
        {
            SetCameraPositionAndRotation();
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // 매 프레임마다 위치와 회전을 업데이트
            SetCameraPositionAndRotation();
        }
    }

    void SetCameraPositionAndRotation()
    {
        // 높낮이 조절을 포함한 카메라 위치 설정
        Vector3 newOffset = offset;
        newOffset.y = heightOffset;
        transform.position = player.position + newOffset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
