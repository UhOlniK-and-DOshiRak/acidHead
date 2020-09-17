using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cash : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int value;
    public int amount;

    private Canvas canvas;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //Debug.Log(rectTransform.anchoredPosition);
        if (rectTransform.anchoredPosition.x > 450f)
        {
            rectTransform.anchoredPosition = new Vector2(450f, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.x < -400f)
        {
            rectTransform.anchoredPosition = new Vector2(-400f, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.y > 185f)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 185f);
        }
        if (rectTransform.anchoredPosition.y < -650f)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -650f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Debug.Log("OnEndDrag");
    }
}
