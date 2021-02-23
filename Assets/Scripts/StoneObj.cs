using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneObj : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public Stone stone;

    bool isTouching = false;
    float timer =0;

    public void Init(int overrideIndex = -1)
    {
        GetComponentInChildren<StoneText>().Init(overrideIndex);
        GetComponentInChildren<StoneIcon>().Init(overrideIndex);

    }

    public void OnDrag(PointerEventData eventData)
    {
        timer =0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        timer = 0;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        timer =0;
        isTouching = false;
    }

    private void Update()
    {
        if(stone.isOkey&&isTouching)
        {
            timer += Time.deltaTime;

            if(timer >= 1.25f)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
                }
                timer =0;
                isTouching = false;
            }
        }

    }
}
