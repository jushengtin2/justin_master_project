using UnityEngine;
using UnityEngine.UI;  // 確保可以使用 UI 元素
using System.Collections;
using TMPro; 
public class breakredlight : MonoBehaviour
{
    // 引用紅燈的 GameObject
    public GameObject redLightObject;

    // 引用需要顯示的 UI Canvas
    public GameObject warningUICanvas;

    // 引用 UI 中的繼續遊戲按鈕
    public Button continueButton;

    public TextMeshProUGUI scoreText; // 記分板的 TextMeshPro 元件
    private int score = 0; // 初始分數為 0

    private void Start()
    {
        // 確保警告 UI 和按鈕開始時是隱藏狀態，並註冊按鈕點擊事件
        if (warningUICanvas != null)
        {
            warningUICanvas.SetActive(false);  // 隱藏 UI
        }
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonClicked);  // 註冊按鈕事件
        }
    }

    // 當觸發器被觸發時的處理邏輯
    private void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發區域的物體是否是車輛（可以通過 Tag 或其他方式確認）
        if (other.CompareTag("Car"))  // 假設你的車輛有 "Player" 標籤
        {
            // 檢查紅燈是否處於活動狀態
            if (redLightObject.activeInHierarchy)
            {
                // 顯示警告 UI 並暫停遊戲
                ShowWarningUI();
                MinusScore();
            }
            else{
                AddScore();//這邊寫加分
            }
        }
    }

    // 顯示警告 UI 並暫停遊戲
    void ShowWarningUI()
    {
        if (warningUICanvas != null)
        {
            warningUICanvas.SetActive(true); // 顯示 UI
            Time.timeScale = 0f;  // 暫停遊戲
            Debug.Log("紅燈亮起 您壓過了指定線 UI 已顯示且遊戲暫停。");
        }
        else
        {
            Debug.LogError("Warning UI Canvas 未設置！");
        }
    }

    // 繼續遊戲邏輯，當按下按鈕時觸發
    public void OnContinueButtonClicked()
    {
        if (warningUICanvas != null)
        {
            warningUICanvas.SetActive(false); // 隱藏 UI
        }
        Time.timeScale = 1f;  // 恢復遊戲
        Debug.Log("遊戲已恢復");
    }

    private void AddScore()
    {
        score += 1; // 分數加 1
        UpdateScoreText(); // 更新記分板上的顯示
    }
    private void MinusScore(){
        if (score>0){

             score -= 1; // 分數-1
        UpdateScoreText(); // 更新記分板上的顯示
        }
       
    }

    private void UpdateScoreText()
    {
        if (scoreText != null && score >=0)
        {
            scoreText.text = "目前分數:  " + score; // 更新 TextMeshPro 上的分數顯示
        }
        
    }
}

