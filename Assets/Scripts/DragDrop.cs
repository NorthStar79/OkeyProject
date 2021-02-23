
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    ItemSlot itemSlotCache;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .6f;
        DragDrop[] dds = FindObjectsOfType<DragDrop>();
        foreach (var item in dds)
        {
           item.canvasGroup.blocksRaycasts = false; 
        }
        transform.SetSiblingIndex(transform.parent.childCount-1);

        ItemSlot[] slots = FindObjectsOfType<ItemSlot>();
        foreach (var item in slots)
        {
            if(item.stoneObj == this.GetComponent<StoneObj>())
            {
                item.stoneObj = null;
                itemSlotCache = item;
            }
        }
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        DragDrop[] dds = FindObjectsOfType<DragDrop>();
        foreach (var item in dds)
        {
           item.canvasGroup.blocksRaycasts = true; 
        }
        
        bool b = false;
        foreach (var item in eventData.hovered)
        {
            if(item.GetComponent<ItemSlot>())
            {
                b = true;
            }
        }
        if(!b)
        {
            rectTransform.anchoredPosition = posCache;
            itemSlotCache.stoneObj = this.GetComponent<StoneObj>();
        }

    }

    public void OnPointerDown(PointerEventData eventData) {
        posCache = rectTransform.anchoredPosition;
    }

    Vector2 posCache;
}
