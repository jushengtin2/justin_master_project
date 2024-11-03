using UnityEngine;
using UnityEngine.UI;

public class OvertakeWarning : MonoBehaviour
{
    public GameObject playerCar; // 主角車輛
    public Canvas warningCanvas; // 警示Canvas Panel
    public Canvas warningCanvas_toofar; // 警示Canvas Panel
    private bool isWarningActive = false;
    private bool isWarningActive_toofar = false;
    void Start()
    {
        if (warningCanvas != null)
        {
            warningCanvas.gameObject.SetActive(false); // 確保Canvas初始是隱藏的
        }

        if (warningCanvas_toofar != null)
        {
            warningCanvas_toofar.gameObject.SetActive(false); // 確保Canvas初始是隱藏的
        }
    }

    void Update()
    {
        if (IsOvertaken() )
        {
            ShowWarning();
        }
        else
        {
            HideWarning();
        }

        if(IsTooFar()){
            ShowWarning2();
        }
        else
        {
            HideWarning2();
        }



    }

    private bool IsOvertaken()
    {
        // 檢查主角車是否在NPC車後面 或者當已經跟丟就gg
        return (transform.position.z - playerCar.transform.position.z) <= 5f ;
    }

    private bool IsTooFar()
    {
        // 檢查主角車是否在NPC車後面 或者當已經跟丟就gg
        return Mathf.Abs(transform.position.z - playerCar.transform.position.z) >= 150f;
    }

    private void ShowWarning()
    {
        if (!isWarningActive && warningCanvas != null)
        {
            warningCanvas.gameObject.SetActive(true);
            isWarningActive = true;
        }
       
    }
    private void ShowWarning2()
    {
        if(!isWarningActive_toofar && warningCanvas_toofar != null){
            warningCanvas_toofar.gameObject.SetActive(true);
            isWarningActive_toofar = true;
        }
    }

    private void HideWarning()
    {
        if (isWarningActive && warningCanvas != null)
        {
            warningCanvas.gameObject.SetActive(false);
            isWarningActive = false;
        }
       
    }
      private void HideWarning2()
    {
        if (isWarningActive_toofar && warningCanvas_toofar != null)
        {
            warningCanvas_toofar.gameObject.SetActive(false);
            isWarningActive_toofar = false;
        }
    }
}
