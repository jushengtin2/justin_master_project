using UnityEditor;
using UnityEngine;

public class RemoveRigidbodiesFromAllObjects : MonoBehaviour
{
    [MenuItem("Tools/Remove Rigidbody from All Objects")]
    private static void RemoveRigidbodies()
    {
        // 獲取場景中所有的 GameObject
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 檢查是否有 Rigidbody，如果有則移除
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                DestroyImmediate(rb);
                Debug.Log($"Removed Rigidbody from {obj.name}");
            }
        }

        Debug.Log("Removed Rigidbody from all objects in the scene.");
    }
}
