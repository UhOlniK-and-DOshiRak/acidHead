using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Paper : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    bool isStuffed;
    GameObject[] positions;
    List<GameObject> items;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    int tabaccoItems;

    private void Start()
    {
        isStuffed = false;
        positions = GameObject.FindGameObjectsWithTag("Tabacco");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        tabaccoItems = 0;
        items = new List<GameObject>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isStuffed)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ItemWorld>() != null && eventData.pointerDrag.GetComponent<ItemWorld>().GetItem().itemType == Item.ItemType.Tabacco)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = positions[tabaccoItems].GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
                eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
                items.Add(eventData.pointerDrag);
                tabaccoItems++;
                if (tabaccoItems > 3)
                {
                    isStuffed = true;
                    transform.Find("Image1").gameObject.SetActive(false);
                    transform.Find("Image2").gameObject.SetActive(true);              
                    foreach (GameObject item in items)
                    {
                        Destroy(item);
                    }
                    items = new List<GameObject>();
                    Workspace.paperInstantiated = false;
                }
                //Destroy(eventData.pointerDrag.gameObject);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent != FindObjectOfType<OrderSlot>().transform)
            canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent != FindObjectOfType<OrderSlot>().transform)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            //if (rectTransform.anchoredPosition.x > 450f)
            //{
            //    rectTransform.anchoredPosition = new Vector2(450f, rectTransform.anchoredPosition.y);
            //}
            if (rectTransform.anchoredPosition.x < -400f)
            {
                rectTransform.anchoredPosition = new Vector2(-400f, rectTransform.anchoredPosition.y);
            }
            if (rectTransform.anchoredPosition.y > 185f)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 185f);
            }
            //if (rectTransform.anchoredPosition.y < -650f)
            //{
            //    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -650f);
            //}
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        if (transform.parent != FindObjectOfType<OrderSlot>().transform)
            canvasGroup.blocksRaycasts = true;
    }

    public bool GetState()
    {
        return isStuffed;
    }
}
