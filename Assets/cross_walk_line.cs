using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cross_walk_line : MonoBehaviour
{
    public WarningUIController Warning_cross_walk_line;  // 引用警示框控制器
    public GameObject redlight;  // 紅燈物件的引用
    private bool isCrossWalkActive = false;  // 控制斑馬線是否應該出現

    void Update()
    {
        // 檢查 redlight 是否啟用來決定 cross_walk_line 的顯示
        if (redlight != null && redlight.activeSelf)
        {
            if (!isCrossWalkActive)
            {
                // 啟用 cross_walk_line
                gameObject.SetActive(true);
                isCrossWalkActive = true;
            }
        }
        else
        {
            if (isCrossWalkActive)
            {
                // 隱藏 cross_walk_line
                gameObject.SetActive(false);
                isCrossWalkActive = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發區域的物體是否是車輛
        if (other.CompareTag("Car"))
        {
            Debug.Log("警告：車輛跨越雙黃線！");

            // 顯示警示框
            if (Warning_cross_walk_line != null)
            {
                Warning_cross_walk_line.ShowWarning();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 檢查離開觸發區域的物體是否是車輛
        if (other.CompareTag("Car"))
        {
            Debug.Log("車輛離開雙黃線區域");

            // 隱藏警示框
            if (Warning_cross_walk_line != null)
            {
                Warning_cross_walk_line.HideWarning();
            }
        }
    }
}
