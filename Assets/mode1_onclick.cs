using UnityEngine;
using UnityEngine.SceneManagement;  // 如果要進行場景切換

public class Button1ClickHandler : MonoBehaviour
{
    // 此方法將與按鈕的OnClick事件連結
    public void OnButton1Click()
    {
        SceneManager.LoadScene("choose_scene-1");  // 切換到名為 "Scene1" 的場景
    }
    public void OnButton2Click()
    {
        SceneManager.LoadScene("Scene2");  // 切換到名為 "Scene2" 的場景
    }
    public void OnButton3Click()
    {
        SceneManager.LoadScene("Scene3");  // 切換到名為 "Scene" 的場景
    }
    public void On_choose_scene1_Button_Click()
    {
        SceneManager.LoadScene("Scene1");  // 切換到名為 "Scene1" 的場景
    }
}