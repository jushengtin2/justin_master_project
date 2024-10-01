using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNode : MonoBehaviour
{
    // 定義上一個和下一個路徑點
    public CarNode previousWaypoint;
    public CarNode nextWaypoint;

    // 定義連接（如你有特殊的分支/交叉點）
    public CarNode link;

    // 返回當前節點的隨機位置（這裡簡化了返回節點的固定位置）
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
