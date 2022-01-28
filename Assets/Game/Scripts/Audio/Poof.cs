using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.ENEMY_HIT, PoofSound);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, PoofSound);
    }

    void PoofSound()
    {
        source.Play();
    }
}
