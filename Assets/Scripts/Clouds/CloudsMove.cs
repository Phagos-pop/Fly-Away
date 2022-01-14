using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMove : MonoBehaviour
{
    public int PositionToDestroy = 12;
    public float Speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        if (transform.position.x > PositionToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
