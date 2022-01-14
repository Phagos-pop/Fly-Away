using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShoot : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit2D = Physics2D.Raycast(p, Vector2.zero);

            if (hit2D.transform != null)
            {
                GameObject hitObject = hit2D.transform.gameObject;
                IEnemy target = hitObject.GetComponent<IEnemy>();
                if (target != null)
                {
                    target.OnHit();
                }
            }
        }
    }
}
