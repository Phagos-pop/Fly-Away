using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void LoadLevel(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
