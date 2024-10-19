using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class RedLightController : MonoBehaviour
{
    public GameObject redLight; // 紅燈物件
    private List<NavMeshAgent> waitingCars = new List<NavMeshAgent>(); // 等待的車輛清單
    public bool is_red_light_or_not = false;

    private void Update()
    {
        // 檢查紅燈是否亮起
        if (IsRedLightActive())
        {
            EnableWaitingLogic(); // 如果紅燈亮起，逐步減速或停止車輛
            is_red_light_or_not = true;
        }
        else
        {
            ReleaseWaitingCars(); // 如果紅燈熄滅，釋放車輛
            is_red_light_or_not = false;
        }
    }

    // 檢查紅燈是否亮起
    public bool IsRedLightActive()
    {
        return redLight != null && redLight.activeSelf;
    }

    private void EnableWaitingLogic()
    {
        // 停止或減速所有進入等待區域的車輛
        foreach (NavMeshAgent car in waitingCars)
        {
            if (car != null && !car.isStopped)
            {
                StartCoroutine(SlowDownCar(car)); // 逐漸減速車輛
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發區域的物件是否標記為 "NPC"
        if (other.CompareTag("NPC"))
        {
            NavMeshAgent carAgent = other.GetComponent<NavMeshAgent>();
            if (carAgent != null && !waitingCars.Contains(carAgent))
            {
                Debug.Log("車輛進入紅燈等待區域");
                waitingCars.Add(carAgent); // 將車輛加入等待清單
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 當車輛離開等待區域時，從等待清單中移除
        if (other.CompareTag("NPC"))
        {
            NavMeshAgent carAgent = other.GetComponent<NavMeshAgent>();
            if (carAgent != null && waitingCars.Contains(carAgent))
            {
                Debug.Log("車輛離開等待區域");
                waitingCars.Remove(carAgent); // 從清單中移除車輛
                carAgent.isStopped = false;   // 恢復車輛行駛
            }
        }
    }

    private void ReleaseWaitingCars()
    {
        // 釋放所有等待的車輛
        foreach (NavMeshAgent carAgent in waitingCars)
        {
            if (carAgent != null)
            {
                carAgent.isStopped = false;  // 恢復車輛行駛
                carAgent.speed = carAgent.speed > 0 ? carAgent.speed : 10f; // 恢复默认速度，3.5为假设的默认值
            }
        }
        waitingCars.Clear();  // 清空等待清單
    }

    private IEnumerator SlowDownCar(NavMeshAgent carAgent)
    {
        float currentSpeed = carAgent.speed; // 獲取當前速度
        float decelerationRate = 3.5f; // 每幀減速速率，可以調整這個數值來改變減速效果
        float minSpeed = 0.1f; // 最小速度，低於這個值將停止車輛

        // 當前速度大於最小速度時，每幀減速
        while (currentSpeed > minSpeed)
        {
            currentSpeed -= decelerationRate * Time.deltaTime; // 逐漸減少速度
            carAgent.speed = Mathf.Max(currentSpeed, minSpeed); // 確保速度不低於最小值
            yield return null; // 等待下一幀
        }

        // 當速度降低到接近0時，完全停止車輛
        carAgent.isStopped = true;
        carAgent.speed = 0; // 確保速度設為0
        Debug.Log("車輛已完全停止");
    }
}