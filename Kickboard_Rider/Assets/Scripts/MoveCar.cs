using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{

    public float speed = 5.0f; // ���� �̵� �ӵ� ����
    public float lifetime = 5.0f; // ���� ���� �ð� ����
    void Start()
    {
        // ���� �ð��� ���� �� ���� ����
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // �� �����Ӹ��� ������ ���� ��ǥ�迡�� ������(����)���� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
