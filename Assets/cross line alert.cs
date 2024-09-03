using UnityEngine;

public class YellowLineDetector : MonoBehaviour
{
    public WarningUIController Warning_cross_yellow_line;  // 引用警示框控制器

    void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發區域的物體是否是車輛
        if (other.CompareTag("Car"))
        {
            Debug.Log("警告：車輛跨越雙黃線！");

            // 顯示警示框
            if (Warning_cross_yellow_line != null)
            {
                Warning_cross_yellow_line.ShowWarning();
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
            if (Warning_cross_yellow_line != null)
            {
                Warning_cross_yellow_line.HideWarning();
            }
        }
    }
}
