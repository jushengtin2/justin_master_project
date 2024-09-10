using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanMovez : MonoBehaviour
{
    public float speed = 5f;  // 物件的運動速度
    private Animator animator;  // 動畫控制器

    void Start()
    {
        // 獲取Animator組件
        animator = GetComponent<Animator>();
        
        // 確保Animator不為空
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on this GameObject.");
        }
    }

    void Update()
    {
        // 持續移動物件
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        
       
        animator.SetBool("isMoving", true);  // 假設有一個參數叫 "isMoving"
       
    }

}
