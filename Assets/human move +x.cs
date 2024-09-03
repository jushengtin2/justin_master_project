using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanMove : MonoBehaviour
{
    public float speed = 5f;  // 物件的運動速度
    public GameObject redlight;  // 目標物件
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
        // 檢查redlight物件是否被啟用
        if (redlight != null && redlight.activeSelf)
        {
            // 如果redlight物件被啟用，則移動物件
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            
            // 啟動動畫
            if (animator != null)
            {
                animator.SetBool("isMoving", true);  // 假設有一個參數叫 "isMoving"
            }
        }
        else
        {
            // 當redlight物件未啟用時，停止動畫
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
