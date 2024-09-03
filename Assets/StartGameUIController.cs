using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  // Add this line to include the IEnumerator namespace

public class StartGameUIController : MonoBehaviour
{
    // 引用開始遊戲的 Canvas
    public GameObject startGameCanvas; // 確保這裡引用的是 Canvas 物件

    public void OnStartGameButtonClicked()
    {
        if (startGameCanvas != null)
        {
            Debug.Log("Canvas 引用已設置，嘗試隱藏 Canvas...");

            // 檢查 Canvas 是否活動
            if (startGameCanvas.activeInHierarchy)
            {
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
        SceneManager.LoadScene("SampleScene"); // 替換 "SampleScene" 為你的遊戲場景名稱
    }
}
