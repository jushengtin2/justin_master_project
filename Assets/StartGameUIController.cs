using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  // Add this line to include the IEnumerator namespace

public class StartGameUIController : MonoBehaviour
{
    // 引用開始遊戲的 Canvas
    public GameObject startGameCanvas; // 確保這裡引用的是 Canvas 物件

    void Start()
    {
        // 一開始遊戲進入暫停狀態
        PauseGame();
    }

    public void OnStartGameButtonClicked()
    {
        if (startGameCanvas != null)
        {
            Debug.Log("Canvas 引用已設置，嘗試隱藏 Canvas...");

            // 檢查 Canvas 是否活動
            if (startGameCanvas.activeInHierarchy)
            {
                // 恢復遊戲
                ResumeGame();

                // 添加一點延遲來確保 Coroutine 在 Canvas 被禁用之前開始
                StartCoroutine(LoadGameScene());

                startGameCanvas.SetActive(false);
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

    private IEnumerator LoadGameScene()
    {
        // 延遲 0.5 秒以確保 Canvas 隱藏後再進行場景切換
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Scene1"); // 替換 "Scene1" 為你的遊戲場景名稱
    }

    // 暫停遊戲方法
    private void PauseGame()
    {
        Time.timeScale = 0f; // 將時間設為 0，遊戲暫停
        Debug.Log("遊戲已暫停");
    }

    // 恢復遊戲方法
    private void ResumeGame()
    {
        Time.timeScale = 1f; // 將時間設為 1，遊戲恢復正常速度
        Debug.Log("遊戲已恢復");
    }
}
