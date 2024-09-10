using System.Collections;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public GameObject redLight;        // 紅燈物體
    public GameObject yellowLight;     // 黃燈物體
    public GameObject greenLight;      // 綠燈物體
    public GameObject yellowLightLine; // 黃燈線物體（用於檢測車輛）
    public GameObject redLightLine;    // 紅燈線物體（用於停止車輛）

    public float redLightDuration = 5f;   // 紅燈持續時間
    public float yellowLightDuration = 0.8f; // 黃燈持續時間
    public float greenLightDuration = 5f; // 綠燈持續時間

    private bool isCarDetectedInYellow = false; // 車輛是否進入黃燈區域

    private void Start()
    {
        StartCoroutine(TrafficLightSequence());
    }

    private IEnumerator TrafficLightSequence()
    {
        while (true)
        {
            // 默認為綠燈
            SwitchLightState(false, false, true);
            SetRedLightLineTag("GreenLight"); // 綠燈時，設置red_light_line為"GreenLight"
            yield return null;

            // 如果檢測到車輛進入yellowLightLine區域，進行黃燈和紅燈循環
            if (isCarDetectedInYellow)
            {
                // 黃燈亮
                SwitchLightState(false, true, false);
                SetRedLightLineTag("YellowLight"); // 黃燈時，保持red_light_line為"YellowLight"
                yield return new WaitForSeconds(yellowLightDuration);

                // 紅燈亮
                SwitchLightState(true, false, false);
                SetRedLightLineTag("RedLight"); // 紅燈時，設置red_light_line為"RedLight"
                yield return new WaitForSeconds(redLightDuration);

                isCarDetectedInYellow = false; // 重置車輛檢測狀態
            }
        }
    }

    private void SwitchLightState(bool red, bool yellow, bool green)
    {
        redLight.SetActive(red);
        yellowLight.SetActive(yellow);
        greenLight.SetActive(green);
    }

    private void SetRedLightLineTag(string tag)
    {
        if (redLightLine != null)
        {
            redLightLine.tag = tag;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發器的物件是否標記為 "Car" 
        // 並且觸發器本身是 yellowLightLine
        if (other.CompareTag("Car") && this.gameObject == yellowLightLine)
        {
            Debug.Log("車輛進入黃燈區域，切換為黃燈和紅燈");
            isCarDetectedInYellow = true; // 設置車輛檢測狀態為真
        }
    }
}
