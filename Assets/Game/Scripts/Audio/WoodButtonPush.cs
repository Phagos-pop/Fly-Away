using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodButtonPush : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.WOOD_BUTTON_PUSH, ButtonSound);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WOOD_BUTTON_PUSH, ButtonSound);
    }

    void ButtonSound()
    {
        source.Play();
    }
}
