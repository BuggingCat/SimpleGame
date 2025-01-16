using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 场景管理

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "SampleScene";
    
    public void continueGame()
    {
        SceneManager.LoadScene(sceneName); // 加载主场景
    }

    public void startGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit(); // 退出程序
    }
}
