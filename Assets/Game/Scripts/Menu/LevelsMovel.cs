using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelsMovel : EventTrigger
{
    private bool dragging;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x,this.transform.position.y);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}
