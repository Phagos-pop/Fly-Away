using UnityEngine;

sealed class BackgroundSounds : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string backgroundMusicTag;

    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(this.backgroundMusicTag);
        if (obj != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.tag = this.backgroundMusicTag;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
