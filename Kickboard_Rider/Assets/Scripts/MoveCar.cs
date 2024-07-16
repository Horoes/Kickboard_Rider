using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{

    public float speed = 5.0f; // 차량 이동 속도 설정
    public float lifetime = 5.0f; // 차량 생존 시간 설정
    void Start()
    {
        // 생존 시간이 지난 후 차량 삭제
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 매 프레임마다 차량을 로컬 좌표계에서 정방향(앞쪽)으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
