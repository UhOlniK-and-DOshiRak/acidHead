using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private List<Item> itemList;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private TextMeshProUGUI weight;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Slider slider;
    public Sprite closedSprite;
    private bool isClosed;

    private void Start()
    {
        itemList = new List<Item>();
        itemSlotContainer = transform.Find("PackageItemContainer");
        itemSlotTemplate = itemSlotContainer.Find("PackageItemTemplate");
        weight = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        slider = transform.Find("Slider").GetComponent<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
        isClosed = false;
    }

    void Update()
    {
        if (slider.value == 100 && slider != null)
        {
            Debug.Log("ClosePackage");
            Image image = transform.Find("Sprite").GetComponent<Image>();
            image.sprite = closedSprite;
            isClosed = true;
            Destroy(slider.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isClosed)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ItemWorld>() != null && eventData.pointerDrag.GetComponent<ItemWorld>().GetItem().itemType != Item.ItemType.Tabacco)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                AddItem(eventData.pointerDrag.GetComponent<ItemWorld>().GetItem());
                Destroy(eventData.pointerDrag.gameObject);
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
            //Debug.Log(rectTransform.anchoredPosition);
            //if (rectTransform.anchoredPosition.x > 450f)
            //{
            //    rectTransform.anchoredPosition = new Vector2(450f, rectTransform.anchoredPosition.y);
            //}
            if (rectTransform.anchoredPosition.x < -330f)
            {
                rectTransform.anchoredPosition = new Vector2(-330f, rectTransform.anchoredPosition.y);
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
        if (transform.parent != FindObjectOfType<OrderSlot>().transform)
            canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;        
    }

    public void AddItem(Item item)
    {
        bool itemAlreadyInPackage= false;
        foreach (Item packageItem in itemList)
        {
            if (packageItem.itemType == item.itemType)
            {
                packageItem.amount += item.amount;
                itemAlreadyInPackage = true;
            }
        }
        if (!itemAlreadyInPackage)
        {
            itemList.Add(item);
        }
        RefreshItems();
    }

    private void RefreshItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotSellSize = 50f;
        int newWeight = 0;

        foreach (Item item in itemList)
        {

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);      

            itemSlotRectTransform.anchoredPosition = new Vector3(x * itemSlotSellSize, y * itemSlotSellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            newWeight += item.GetWeight() * item.amount;
            //TextMeshProUGUI text = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            //if (item.amount > 1)
            //{
            //    text.SetText(item.amount.ToString());
            //}
            //else
            //{
            //    text.SetText("");
            //}
            x++;
            if (x > 2)
            {
                x = 0;
                y--;
            }
        }

        weight.SetText(newWeight.ToString() + " мг");
    }

    public bool GetState()
    {
        return isClosed;
    }

    public List<Item> GetItems()
    {
        return itemList;
    }
}
