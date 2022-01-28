using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string backgroundMusicTag;
    
    void Awake()
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
