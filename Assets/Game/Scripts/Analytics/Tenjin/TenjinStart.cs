using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinStart : MonoBehaviour
{
    void Start()
    {
        TenjinConnect();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            TenjinConnect();
        }
    }

    public void TenjinConnect()
    {
        BaseTenjin instance = Tenjin.getInstance("C1VYE96SJFZIOAGY7NTS96SFAQGXWEHY");
        instance.Connect();
    }
}
