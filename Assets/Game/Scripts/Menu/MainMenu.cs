using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void LoadLevel(int number)
    {
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        SceneManager.LoadScene(number);
    }

    public void ExitGame()
    {
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        Application.Quit();
    }

    public void WoodButtonPush()
    {
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        
    }
}
