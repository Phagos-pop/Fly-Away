using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string analyticsTag;

    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(this.analyticsTag);
        if (obj != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.tag = this.analyticsTag;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
