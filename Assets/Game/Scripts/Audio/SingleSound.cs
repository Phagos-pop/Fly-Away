using UnityEngine;

sealed class SingleSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private string EventType;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Messenger.AddListener(EventType, PlaySound);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(EventType, PlaySound);
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
