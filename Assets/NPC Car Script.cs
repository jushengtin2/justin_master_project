using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCarScript : MonoBehaviour
{
    public Transform[] waypoints;  // 路徑點
    public Transform playerCar;    // 主角車位置，用來檢測是否需要避開
    private int currentWaypoint = 0; // 當前目標點
    private NavMeshAgent agent;

    public float obstacleDetectionDistance = 5f;  // 檢測障礙物的距離
    public float stopDistance = 2f;  // 停止距離
    public float slowDownSpeed = 5f; // 遇到障礙物時的減速速度
    public LayerMask obstacleLayer;  // 檢測障礙物的圖層

    private float originalSpeed;     // 保存原始速度

    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    originalSpeed = agent.speed; // 保存原本的速度
    CalculateAndSetPath();
}

void Update()
{
    DetectAndAvoidObstacle();

    // 檢查是否到達當前目標點
    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    {
        GoToNextWaypoint();
    }
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

// 重新計算路徑，確保 NPC 重新根據連接線導航
void DetectAndAvoidObstacle()
{
    // 使用 Raycast 檢測前方是否有障礙物
    RaycastHit hit;
    Vector3 forward = transform.TransformDirection(Vector3.forward);

    // 從 NPC 車輛向前發射一個 Ray，檢測是否有障礙物（如主角車）
    if (Physics.Raycast(transform.position, forward, out hit, obstacleDetectionDistance, obstacleLayer))
    {
        // 檢測到障礙物（主角車）
        if (hit.collider.gameObject.CompareTag("Car")) // 確認前方是主角車
        {
            // 如果距離小於停止距離，完全停止
            if (hit.distance <= stopDistance)
            {
                agent.isStopped = true;
            }
            // 如果距離大於停止距離但小於檢測距離，則減速並嘗試繞過
            else
            {
                agent.isStopped = false;
                agent.speed = slowDownSpeed;  // 減速

                // 嘗試繞過障礙物
                Vector3 avoidDirection = Vector3.Cross(forward, Vector3.up); // 計算繞開方向
                Vector3 newTarget = transform.position + avoidDirection * 2f; // 繞過的目標點

                // 設定新的目標點，嘗試繞開
                agent.SetDestination(newTarget);
            }
        }
    }
    else
    {
        // 沒有檢測到障礙物，恢復正常速度，並重新計算路徑
        agent.isStopped = false;
        agent.speed = originalSpeed;

        // 重新計算路徑
        CalculateAndSetPath();
    }
}

void GoToNextWaypoint()
{
    if (waypoints.Length == 0)
        return;

    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;

    // 計算並設定下一個路徑
    CalculateAndSetPath();
}

// 在 Scene 視圖中繪製路徑和檢測範圍
private void OnDrawGizmos()
{
    if (waypoints.Length == 0)
        return;

    Gizmos.color = Color.white;

    // 繪製路徑點
    for (int i = 0; i < waypoints.Length; i++)
    {
        if (waypoints[i] != null)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.5f);

            if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }

    // 繪製最後一個點到第一個點的連線
    if (waypoints[waypoints.Length - 1] != null && waypoints[0] != null)
    {
        Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
    }

    // 繪製檢測障礙物的 Raycast 距離
    Gizmos.color = Color.red;
    Vector3 forward = transform.TransformDirection(Vector3.forward) * obstacleDetectionDistance;
    Gizmos.DrawRay(transform.position, forward);
}
}
