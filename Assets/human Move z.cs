using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanMovez : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;  // 車輛的運動速度

    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }
}
