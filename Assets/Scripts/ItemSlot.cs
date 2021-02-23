
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler,IPointerExitHandler,IPointerEnterHandler
{

    public StoneObj stoneObj;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            stoneObj = eventData.pointerDrag.GetComponent<StoneObj>();
        }
    }

    [ContextMenu("SlideRight")]
    void SlideRi()
    {
        SlideRight(this);
    }

    [ContextMenu("SlideLeft")]
    void SlideLe()
    {
        SlideLeft(this);
    }

    public void SlideRight(ItemSlot itemSlot)
    {
        if(stoneObj ==null) return;
        int i = itemSlot.transform.GetSiblingIndex();
        Transform parent = itemSlot.transform.parent;
        if(parent.childCount-1<=i)
        {
            //parent.GetChild(i - 1).GetComponent<ItemSlot>().SlideLeft(parent.GetChild(i - 1).GetComponent<ItemSlot>());
            SlideLeft(this);
        }
        else
        {
            if (parent.GetChild(i + 1).GetComponent<ItemSlot>().stoneObj == null)
            {
                parent.GetChild(i + 1).GetComponent<ItemSlot>().stoneObj = stoneObj;
                parent.GetChild(i + 1).GetComponent<ItemSlot>().Snap();
                stoneObj = null;

            }
            else
            {
                parent.GetChild(i + 1).GetComponent<ItemSlot>().SlideRight(parent.GetChild(i + 1).GetComponent<ItemSlot>());
                parent.GetChild(i + 1).GetComponent<ItemSlot>().Snap();
                parent.GetChild(i + 1).GetComponent<ItemSlot>().stoneObj = stoneObj;
                stoneObj = null;
            }
        }

        Invoke("Snap",.1f);

    }
    public void SlideLeft(ItemSlot itemSlot)
    {
        if(stoneObj ==null) return;
        int i = itemSlot.transform.GetSiblingIndex();
        Transform parent = itemSlot.transform.parent;
        if(i<=0)
        {
            //parent.GetChild(i + 1).GetComponent<ItemSlot>().SlideRight(parent.GetChild(i + 1).GetComponent<ItemSlot>());
            SlideRight(this);
        }
        else
        {
            if (parent.GetChild(i - 1).GetComponent<ItemSlot>().stoneObj == null)
            {
                parent.GetChild(i - 1).GetComponent<ItemSlot>().stoneObj = stoneObj;
                parent.GetChild(i - 1).GetComponent<ItemSlot>().Snap();
                stoneObj = null;

            }
            else
            {
                parent.GetChild(i - 1).GetComponent<ItemSlot>().SlideLeft(parent.GetChild(i - 1).GetComponent<ItemSlot>());
                parent.GetChild(i - 1).GetComponent<ItemSlot>().Snap();
                parent.GetChild(i - 1).GetComponent<ItemSlot>().stoneObj = stoneObj;
                stoneObj = null;
            }
        }

        Invoke("Snap",.1f);
    }

    private void Update()
    {
        //Snap();
    }

    void Snap()
    {
        if (stoneObj != null)
            stoneObj.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    bool isSlided = false,isLeft= false;

    
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isSlided) return;
        
        if(eventData.dragging)
        {
        int i = transform.GetSiblingIndex();
        Transform parent =transform.parent;


            if(isLeft)
            {
                //SlideRi();
                if(transform.GetSiblingIndex()>0)
                parent.GetChild(i - 1).GetComponent<ItemSlot>().SlideRi();
                else
                parent.GetChild(i + 1).GetComponent<ItemSlot>().SlideLe();
            }else
            {
                //SlideLe();
                if(transform.GetSiblingIndex()<parent.childCount-1)
                parent.GetChild(i + 1).GetComponent<ItemSlot>().SlideLe();
                else
                parent.GetChild(i - 1).GetComponent<ItemSlot>().SlideRi();
            }
            isSlided = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            if(eventData.position.x > GetComponent<RectTransform>().position.x+54)
            {
                SlideRi();
                isLeft = false;
            }
            else
            {
                SlideLe();
                isLeft = true;
            }
            isSlided = true;
        }
    }

}
