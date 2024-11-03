using System;
using UnityEngine;
using UnityEngine.InputSystem; // 引入 Input System

[RequireComponent(typeof(Rigidbody))] // 確保遊戲物件上附有剛體組件
public class WASDController : MonoBehaviour
{
    public float moveSpeed = 5.0f;   // 前進和後退的速度
    public float turnSpeed = 150.0f; // 旋轉的速度

    private Rigidbody rb; // 剛體組件
    public GameObject scene2_waring_line; // 關卡2 的觸發線 會跳出警告保持車距視窗
    public GameObject scene2_finish_line; // 關卡2 的觸發線 會跳出警告保持車距視窗

    public GameObject scene2_waring_canvas; // 關卡2 的觸發線 會跳出警告保持車距視窗

    public GameObject scene2_finish_game_canvas; // 關卡2 的觸發線 會結束遊戲
    public InputAction steerAction;
    
    void Start()
    {
        // 獲取剛體組件
        rb = GetComponent<Rigidbody>();
        scene2_waring_canvas.SetActive(false);
        scene2_finish_game_canvas.SetActive(false);
    }

    

    void FixedUpdate ()
    {
        // Forward and backward movement using W and S keys
        float moveZ = Input.GetAxis("Vertical"); // W/S keys or Up/Down arrow keys
        if (Mathf.Abs(moveZ) > 0.01f) // Move if the input is above a small threshold
        {
            Vector3 movement = transform.forward * moveZ * moveSpeed * Time.deltaTime ;
            rb.MovePosition(rb.position + movement);
        }

        // Left and right rotation using A and D keys
        float turnHorizontal = Input.GetAxis("Horizontal"); // A/D keys or Left/Right arrow keys
        float rotation = turnHorizontal * turnSpeed * Time.deltaTime ;
        Quaternion turn = Quaternion.Euler(0, rotation, 0);
        rb.MoveRotation(rb.rotation * turn);
    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Obstacle"))  // 確認對象是障礙物
    {   Debug.Log("撞到");
        Rigidbody obstacleRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (obstacleRigidbody != null)

        {
            // 獲取碰撞點的法線方向，並施加力讓障礙物飛出去
            Vector3 forceDirection = new Vector3(0f, 0f, 1f); // 將力施加於 z 軸方向
            float forceMagnitude = 1000f;
            obstacleRigidbody.AddForce(forceDirection * forceMagnitude);
        }
    }
}


   private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("scene2_waring_line")) // Check if the trigger object has the "scene2_waring_line" tag
    {
        ShowDialog();
    }
    else if (other.CompareTag("scene2_finish_line")) // Check if the trigger object has the "scene2_finish_line" tag
    {
        FinishGame();
    }
}
    void ShowDialog()
    {
        if (scene2_waring_canvas != null)
        {
            scene2_waring_canvas.SetActive(true); // 顯示 UI.

            Debug.Log("open UI!!");
            PauseGame();
        }
    }


   

    private void FinishGame()
{
    if (scene2_finish_game_canvas != null)
    {
        
        scene2_finish_game_canvas.SetActive(true); // Show the finish game UI
        Debug.Log("Finish game UI opened!");
    }
}
    

    private void PauseGame()
    {
        Time.timeScale = 0f; // 將時間設為 0，遊戲暫停
        Debug.Log("遊戲已暫停");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // 將時間設為 1，遊戲恢復正常速度
        Debug.Log("遊戲已恢復");
    }

    public void OnStartGameButtonClicked()
    {
        if (scene2_waring_canvas != null)
        {
            Debug.Log("Canvas 引用已設置，嘗試隱藏 Canvas...");

            // 檢查 Canvas 是否活動
            if (scene2_waring_canvas.activeInHierarchy)
            {
                // 恢復遊戲
                ResumeGame();

               

                scene2_waring_canvas.SetActive(false);
                Debug.Log("Canvas 已隱藏");
            }
            else
            {
                Debug.Log("Canvas 已經是隱藏狀態");
            }
        }
        else
        {
            Debug.LogError("Canvas 引用未設置！");
        }
    }
}
