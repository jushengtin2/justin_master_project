using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // 車輛的變換（Transform）引用
    public Vector3 offset;           // 攝影機與車輛的初始偏移量
    public float followSpeed = 10f;  // 跟隨速度
    public float rotationSpeed = 5f; // 攝影機旋轉速度

    void Start()
    {
        // 初始化攝影機的偏移量，如果你希望使用當前位置作為偏移量
        if (offset == Vector3.zero && target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 使用車輛的旋轉計算攝影機的新位置，使攝影機始終在車輛後方
            Vector3 desiredPosition = target.position + target.rotation * offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            
            // 更新攝影機的位置
            transform.position = smoothedPosition;

            // 平滑攝影機旋轉，使其始終面向車輛前方
            Quaternion targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
