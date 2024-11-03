using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGameUIController : MonoBehaviour
{
    // References to start game Canvases
    public GameObject startGameCanvas;
    public GameObject startGameCanvas_just4start;
    public GameObject startGameCanvas_just4end;

    public GameObject startGameCanvas_toofar;
    void Start()
    {
        // Pause the game at the start
        PauseGame();
    }

    public void OnStartGameButtonClicked() //這第一關的
    {
        if (startGameCanvas != null)
        {
            Debug.Log("Canvas reference set, attempting to hide Canvas...");

            // Check if Canvas is active
            if (startGameCanvas.activeInHierarchy)
            {
                ResumeGame();
                StartCoroutine(LoadGameScene("Scene1")); // Replace "Scene1" with your main game scene name
                startGameCanvas.SetActive(false);
                Debug.Log("Canvas hidden");
            }
            else
            {
                Debug.Log("Canvas is already hidden");
            }
        }
        else
        {
            Debug.LogError("Canvas reference not set!");
        }
    }

    public void OnStartGameButtonClicked_toofar()
    {
        if (startGameCanvas_toofar != null)
        {
            Debug.Log("Canvas reference set, attempting to hide Canvas...");

            // Check if Canvas is active
            if (startGameCanvas_toofar.activeInHierarchy)
            {   Time.timeScale = 1f;  // 確保遊戲沒有暫停
                StartCoroutine(LoadGameScene("Scenes/choose_scene-1")); // Replace "Scene2" with another scene name
                Debug.Log("123456654");
            }
            else
            {
                Debug.Log("Canvas is already hidden");
            }
        }
        else
        {
            Debug.LogError("Canvas reference not set!");
        }
    }

    public void OnStartGameButtonClicked_just4start()
    {
        if (startGameCanvas_just4start != null)
        {
            Debug.Log("Canvas reference set, attempting to hide Canvas...");

            // Check if Canvas is active
            if (startGameCanvas_just4start.activeInHierarchy)
            {
                
                startGameCanvas_just4start.SetActive(false);
                Debug.Log("Canvas hidden");
            }
            else
            {
                Debug.Log("Canvas is already hidden");
            }
        }
        else
        {
            Debug.LogError("Canvas reference not set!");
        }
    }

    public void OnStartGameButtonClicked_just4end()
    {
        if (startGameCanvas_just4end != null)
        {
            Debug.Log("Canvas reference set, attempting to hide Canvas...");

            // Check if Canvas is active
            if (startGameCanvas_just4end.activeInHierarchy)
            {   Time.timeScale = 1f;  // 確保遊戲沒有暫停
                StartCoroutine(LoadGameScene("Scenes/choose_scene-1")); // Replace "Scene2" with another scene name
                Debug.Log("123456654");
            }
            else
            {
                Debug.Log("Canvas is already hidden");
            }
        }
        else
        {
            Debug.LogError("Canvas reference not set!");
        }
    }

    // New method to load different scenes by name
    public void OnButtonClickLoadScene(string sceneName="Scene1")
    {
        Debug.Log("Loading scene: " + sceneName);
        StartCoroutine(LoadGameScene(sceneName));
    }

    private IEnumerator LoadGameScene(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game paused");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game resumed");
    }
}
