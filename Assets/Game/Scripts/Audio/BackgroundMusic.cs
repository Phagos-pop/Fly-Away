using System.Collections;
using UnityEngine;

sealed class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float volumeReduction;
    [SerializeField] private float volumeReductionTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.WIN, SoundReduction);
        Messenger.AddListener(GameEvent.DEFEAT, SoundReduction);
    }

    private void SoundReduction()
    {
        StartCoroutine(SoundReductionCoroutine());
    }

    private IEnumerator SoundReductionCoroutine()
    {
        audioSource.volume = volumeReduction;
        yield return new WaitForSeconds(volumeReductionTime);
        audioSource.volume = 1f;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WIN, SoundReduction);
        Messenger.RemoveListener(GameEvent.DEFEAT, SoundReduction);
    }
}
