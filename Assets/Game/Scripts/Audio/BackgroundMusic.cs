using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.WIN, WinSound);
        Messenger.AddListener(GameEvent.DEFEAT, WinSound);
    }

    void WinSound()
    {
        StartCoroutine(SoundReduction());
    }

    IEnumerator SoundReduction()
    {
        source.volume = 0.40f;
        yield return new WaitForSeconds(2f);
        source.volume = 1f;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WIN, WinSound);
        Messenger.RemoveListener(GameEvent.DEFEAT, WinSound);
    }
}
