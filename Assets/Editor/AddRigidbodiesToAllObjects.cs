using UnityEditor;
using UnityEngine;

public class AddRigidbodiesToAllObjects : MonoBehaviour
{
    [MenuItem("Tools/Add Rigidbody to All Objects")]
    private static void AddRigidbodies()
    {
        // 獲取場景中所有的 GameObject
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 檢查是否已經有 Rigidbody，如果沒有則添加
            if (obj.GetComponent<Rigidbody>() == null)
            {
                obj.AddComponent<Rigidbody>();
                Debug.Log($"Added Rigidbody to {obj.name}");
            }
        }

        Debug.Log("Added Rigidbody to all objects in the scene.");
    }
}
