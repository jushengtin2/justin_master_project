using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    public float speed = 5f;  // 車輛的初始運動速度
    public float decelerationRate = 2f;  // 減速率
    public Transform pathPoint;  // 目標路徑點
    private float currentSpeed;  // 當前速度
    private bool shouldStop = false;  // 是否應該停止
    private Rigidbody rb;  // 剛體組件
    public float correctionStrength = 5f;  // 路線修正強度

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 獲取車輛的剛體
        currentSpeed = speed;  // 初始化當前速度
    }

    void Update()
    {
        // 根據是否應該停止來計算當前速度
        if (shouldStop)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, decelerationRate * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, decelerationRate * Time.deltaTime);
        }

        // 計算修正方向
        Vector3 directionToPath = (pathPoint.position - transform.position).normalized;
        Vector3 correctionDirection = Vector3.Lerp(transform.forward, directionToPath, correctionStrength * Time.deltaTime);

        // 計算移動方向
        Vector3 movement = correctionDirection * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement); // 使用剛體移動車輛

        // 修正車輛方向以面向路徑
        Quaternion targetRotation = Quaternion.LookRotation(correctionDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, correctionStrength * Time.deltaTime);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GreenLight"))
        {
            Debug.Log("離開紅燈區域");
            shouldStop = false;
        }
        else if (other.CompareTag("RedLight"))
        {
            Debug.Log("撞到紅燈");
            shouldStop = true;
        }
    }
}
