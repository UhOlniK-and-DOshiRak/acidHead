using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (rectTransform.parent.GetComponent<Paper>() == null)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform.parent.GetComponent<Paper>() == null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            if (rectTransform.anchoredPosition.x > 400f)
            {
                rectTransform.anchoredPosition = new Vector2(400f, rectTransform.anchoredPosition.y);
            }
            if (rectTransform.anchoredPosition.x < -330f)
            {
                rectTransform.anchoredPosition = new Vector2(-330f, rectTransform.anchoredPosition.y);
            }
            if (rectTransform.anchoredPosition.y > 180f)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 180f);
            }
            if (rectTransform.anchoredPosition.y < -650f)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -650f);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (rectTransform.parent.GetComponent<Paper>() == null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
