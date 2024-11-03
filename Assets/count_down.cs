using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;  // 設定倒數計時的 TextMeshPro 元件
    public Button confirmButton;    // 確認按鈕
    public int countdownTime = 3;   // 倒數時間（可以自定義）

    void Start()
    {
        // 初始化時不開始倒數，等待按下確認按鈕
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    void OnConfirmButtonClicked()
    {
        // 隱藏確認按鈕
        confirmButton.gameObject.SetActive(false);

        // 開始倒數並暫停遊戲
        PauseGame();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        int currentCount = countdownTime;
        
        // 倒數計時
        while (currentCount > 0)
        {
            countdownText.text = currentCount.ToString();  // 更新倒數數字
            yield return new WaitForSecondsRealtime(1f);   // 使用實際時間等待 1 秒
            currentCount--;
        }

        countdownText.text = "Go!";                       // 顯示 "Go!" 表示遊戲開始
        yield return new WaitForSecondsRealtime(1f);      // 顯示 "Go!" 一秒
        countdownText.gameObject.SetActive(false);        // 隱藏倒數文字

        // 恢復遊戲的時間縮放
        ResumeGame();
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
}
