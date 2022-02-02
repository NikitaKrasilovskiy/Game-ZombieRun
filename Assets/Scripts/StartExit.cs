using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartExit : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("01_LifeIsRunning");
    }
    public void Menu()
    {
        SceneManager.LoadScene("0_Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
}