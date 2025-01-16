using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ��������

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "SampleScene";
    
    public void continueGame()
    {
        SceneManager.LoadScene(sceneName); // ����������
    }

    public void startGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit(); // �˳�����
    }
}
