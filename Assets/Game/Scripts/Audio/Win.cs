using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.WIN, WinSound);
    }

    void WinSound()
    {
        source.Play();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WIN, WinSound);
    }
}
