using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空間

public class Continuegame : MonoBehaviour
{
    public GameObject pauseMenuUI; // 連接到UI上的Pause Menu物件
    public Button resumeButton;    // 連接到按鈕的Button組件

    void Start()
    {
        // 確保按鈕被正確連接並監聽按鈕點擊事件
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
    }

    // 恢復遊戲的方法
    public void ResumeGame()
    {
        // 隱藏UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // 恢復遊戲
        Time.timeScale = 1f;
    }
}


