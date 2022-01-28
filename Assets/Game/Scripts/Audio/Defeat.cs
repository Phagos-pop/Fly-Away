using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.DEFEAT, DefeatSound);
    }

    void DefeatSound()
    {
        source.Play();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.DEFEAT, DefeatSound);
    }
}
