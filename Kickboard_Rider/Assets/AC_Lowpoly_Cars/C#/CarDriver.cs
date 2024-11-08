using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheelCollider;
    public WheelCollider rightWheelCollider;
    public GameObject leftWheelMesh;
    public GameObject rightWheelMesh;
    public bool motor;
    public bool steering;
}

public class CarDriver : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float brakeTorque;
    public float decelerationForce;

    // 고정된 이동 속도 추가
    public float fixedSpeed = 10f;

    // Rigidbody 컴포넌트를 참조하기 위한 변수 추가
    private Rigidbody rb;

    void Start()
    {
        // Rigidbody 컴포넌트를 가져와서 중력을 비활성화
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    public void ApplyLocalPositionToVisuals(AxleInfo axleInfo)
    {
        Vector3 position;
        Quaternion rotation;
        axleInfo.leftWheelCollider.GetWorldPose(out position, out rotation);
        axleInfo.leftWheelMesh.transform.position = position;
        axleInfo.leftWheelMesh.transform.rotation = rotation;
        axleInfo.rightWheelCollider.GetWorldPose(out position, out rotation);
        axleInfo.rightWheelMesh.transform.position = position;
        axleInfo.rightWheelMesh.transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        // 입력 대신 고정된 속도로 이동하도록 변경
        float motor = fixedSpeed; // 고정된 속도로 이동
        float steering = 0; // 방향 변경 없음

        for (int i = 0; i < axleInfos.Count; i++)
        {
            if (axleInfos[i].steering)
            {
                Steering(axleInfos[i], steering);
            }
            if (axleInfos[i].motor)
            {
                Acceleration(axleInfos[i], motor);
            }
            ApplyLocalPositionToVisuals(axleInfos[i]);
        }
    }

    private void Acceleration(AxleInfo axleInfo, float motor)
    {
        if (motor != 0f)
        {
            axleInfo.leftWheelCollider.brakeTorque = 0;
            axleInfo.rightWheelCollider.brakeTorque = 0;
            axleInfo.leftWheelCollider.motorTorque = motor;
            axleInfo.rightWheelCollider.motorTorque = motor;
        }
        else
        {
            Deceleration(axleInfo);
        }
    }

    private void Deceleration(AxleInfo axleInfo)
    {
        axleInfo.leftWheelCollider.brakeTorque = decelerationForce;
        axleInfo.rightWheelCollider.brakeTorque = decelerationForce;
    }

    private void Steering(AxleInfo axleInfo, float steering)
    {
        axleInfo.leftWheelCollider.steerAngle = steering;
        axleInfo.rightWheelCollider.steerAngle = steering;
    }

    private void Brake(AxleInfo axleInfo)
    {
        axleInfo.leftWheelCollider.brakeTorque = brakeTorque;
        axleInfo.rightWheelCollider.brakeTorque = brakeTorque;
    }
}
