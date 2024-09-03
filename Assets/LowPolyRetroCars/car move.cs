using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // 確保遊戲物件上附有剛體組件
public class WASDController : MonoBehaviour
{
    public float moveSpeed = 5.0f;   // 前進和後退的速度
    public float turnSpeed = 100.0f; // 旋轉的速度

    private Rigidbody rb; // 剛體組件

    void Start()
    {
        // 獲取剛體組件
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 取得前進（W和S鍵）輸入
        float moveVertical = Input.GetAxis("Vertical"); // W/S 或者 Up/Down 鍵

        // 取得旋轉（A和D鍵）輸入
        float turnHorizontal = Input.GetAxis("Horizontal"); // A/D 或者 Left/Right 鍵

        // 使用剛體移動物件
        Vector3 movement = transform.forward * moveVertical * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        // 使用剛體旋轉物件
        float rotation = turnHorizontal * turnSpeed * Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0, rotation, 0);
        rb.MoveRotation(rb.rotation * turn);
    }
}
