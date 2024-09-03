using UnityEngine;

public class WarningUIController : MonoBehaviour
{
    public GameObject warningPanel;  // 拖放警示框 Panel 到這個欄位

    void Start()
    {
        // 確保警示框 Panel 一開始是隱藏的
        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }

    public void ShowWarning()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
        }
    }

    public void HideWarning()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }
}
