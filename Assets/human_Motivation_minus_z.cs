using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanMotivation_minus_z : MonoBehaviour
{
    public float speed = 5f;  // 車輛的運動速度

    void Update()
    {

        // 移動物體
        transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.World);
    }
}
