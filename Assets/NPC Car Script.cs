using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCCarScript : MonoBehaviour
{
    public Transform[] waypoints;  // 路徑點
    private int currentWaypoint = 0; // 當前目標點
    private NavMeshAgent agent;

    public float obstacleDetectionDistance = 1f;  // 檢測障礙物的距離
    public float stopDistance = 8f;  // 停止距離
    public float slowDownSpeed = 10f; // 遇到障礙物時的減速速度
    public LayerMask obstacleLayer;  // 檢測障礙物的圖層
    public RedLightController redLightController;  // 引用紅燈控制器

    private float originalSpeed;     // 保存原始速度

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = agent.speed; // 保存原本的速度
        CalculateAndSetPath();
    }

    void Update()
    {
        // 檢查紅燈狀態，如果紅燈亮起，則暫停 NPC 的移動
        if (redLightController != null && redLightController.IsRedLightActive())
        {
            agent.isStopped = true; // 停止車輛
            return; // 不繼續執行後續的路徑更新邏輯
        }

        // 如果紅燈未亮起，檢查是否到達當前目標點，然後更新到下一個目標
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            GoToNextWaypoint();
        }

        // 障礙物檢測與避讓
        //DetectAndAvoidObstacle();
    }

    // 計算並設定 NPC 的行駛路徑
    void CalculateAndSetPath()
    {
        if (waypoints.Length == 0)
            return;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(waypoints[currentWaypoint].position, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {   
            agent.SetPath(path);
        }
    }

    // 障礙物檢測與避讓
    void DetectAndAvoidObstacle()
    {
        bool obstacleDetected = false;

        int numRays = 7;  // 射線的數量
        float angleStep = 60f / (numRays - 1);
        float startAngle = -30f;

        for (int i = 0; i < numRays; i++)
        {
            float currentAngle = startAngle + i * angleStep;
            Vector3 rayDirection = Quaternion.Euler(0, currentAngle, 0) * transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, obstacleDetectionDistance, obstacleLayer))
            {
                if (hit.collider.gameObject.CompareTag("Car"))
                {
                    obstacleDetected = true;

                    if (hit.distance <= stopDistance)
                    {
                        agent.isStopped = true;
                    }
                    else
                    {
                        agent.isStopped = false;
                        agent.speed = slowDownSpeed;

                        Vector3 avoidDirection = Vector3.Cross(transform.forward, Vector3.up);
                        Vector3 newTarget = transform.position + avoidDirection * 2f;
                        agent.SetDestination(newTarget);
                    }

                    break; // 如果檢測到障礙物，跳出迴圈
                }
            }

            Debug.DrawRay(transform.position, rayDirection * obstacleDetectionDistance, Color.red);
        }

        if (!obstacleDetected)
        {
            agent.isStopped = false; // 確保 NPC 可以繼續前進
            agent.speed = originalSpeed;
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        currentWaypoint = (currentWaypoint + 1) % waypoints.Length; // 循環路徑點
        CalculateAndSetPath();
    }
}
