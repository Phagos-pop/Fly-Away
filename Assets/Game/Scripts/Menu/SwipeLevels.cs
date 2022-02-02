using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeLevels : MonoBehaviour , IBeginDragHandler , IDragHandler
{
    [SerializeField]
    private GameObject[] levels;
    private int numLevels;
    private int numOfPages;
    private int currentPage;

    private void Start()
    {
        numLevels = 1;
        for (int i = 0; i < levels.Length; i++)
        {
            for (int j = 0; j < levels[i].transform.childCount; j++)
            {
                GameObject button = levels[i].transform.GetChild(j).gameObject as GameObject;
                button.name = ($"Levels {numLevels}");
                button.GetComponent<Button>().onClick.AddListener(() => { 
                    MainMenu.LoadLevel(Convert.ToInt32(button.transform.GetChild(0).gameObject.GetComponent<Text>().text)); 
                }
                );
                button.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"{numLevels}";
                numLevels++;
            }
        }
        currentPage = 1;
        numOfPages = levels.Length;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                MovePanels(-1);

            }
            else
            {
                MovePanels(1);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
    public void MovePanels(int direction)
    {
        if ((currentPage == 1 && direction == -1) || (currentPage == numOfPages && direction == 1))
        {
            return;
        }
        for (int i = 0; i < levels.Length; i++)
        {
            //levels[i].transform.SetPositionAndRotation((levels[i].transform.position + new Vector3(23.1481f * -direction, 0f, 0f)), Quaternion.identity);

        }
        currentPage += direction;
    }

    //IEnumerator MovePanels(int direction)
    //{
    //    if ((currentPage == 1 && direction == -1) || (currentPage == numOfPages && direction == 1))
    //    {
    //        yield return break;
    //    }
    //    for (int i = 0; i < levels.Length; i++)
    //    {
    //        //levels[i].transform.SetPositionAndRotation((levels[i].transform.position + new Vector3(23.1481f * -direction, 0f, 0f)), Quaternion.identity);
    //        levels[i].transform.Translate
    //    }
    //    currentPage += direction;
    //}

}
