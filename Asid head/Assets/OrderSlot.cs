using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private List<ItemSlot> itemSlots;
    [SerializeField] private List<Paper> papers;
    [SerializeField] private List<Item> items;
    public GameObject label;
    //bool isPackage;

        public int GetPapersCount()
    {
        return papers.Count;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        items = new List<Item>();
        itemSlots = new List<ItemSlot>();
        papers = new List<Paper>();
        SetLabel();
        //isPackage = false;
    }

    public List<Item> GetItems() {
        return items;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<ItemSlot>() != null /*&& !isPackage*/ && eventData.pointerDrag.GetComponent<ItemSlot>().GetState())
        {
            Attach(eventData);
            List<Item> itemsSlot = eventData.pointerDrag.GetComponent<ItemSlot>().GetItems();
            foreach (Item itemSlot in itemsSlot)
            {
                bool itemAlreadyInOrder = false;
                foreach (Item item in items)
                {
                    if (item.itemType == itemSlot.itemType)
                    {
                        item.amount += itemSlot.amount;
                        itemAlreadyInOrder = true;
                    }
                }
                if (!itemAlreadyInOrder)
                {
                    items.Add(itemSlot);
                }
            }
            
            itemSlots.Add(eventData.pointerDrag.GetComponent<ItemSlot>());
        }
        if (eventData.pointerDrag.GetComponent<Paper>() != null && eventData.pointerDrag.GetComponent<Paper>().GetState())
        {
            Attach(eventData);
            papers.Add(eventData.pointerDrag.GetComponent<Paper>());
        }
        SetLabel();
    }

    private void Attach(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().SetParent(transform);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetLabel();
    }

    public void ClearSlot()
    {
        foreach (ItemSlot itemSlot in itemSlots)
        {
            itemSlot.GetComponent<CanvasGroup>().blocksRaycasts = true;
            itemSlot.GetComponent<RectTransform>().SetParent(GameObject.Find("ItemWorldContainer").transform);
            itemSlot.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("PosPackage").GetComponent<RectTransform>().anchoredPosition;
        }
        foreach (Paper paper in papers)
        {
            paper.GetComponent<CanvasGroup>().blocksRaycasts = true;
            paper.GetComponent<RectTransform>().SetParent(GameObject.Find("ItemWorldContainer").transform);
            paper.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("PosPaper").GetComponent<RectTransform>().anchoredPosition;
        }
        itemSlots = new List<ItemSlot>();
        items = new List<Item>();
        papers = new List<Paper>();
        SetLabel();
    }

    public void SetLabel()
    {
        if (items.Count <= 0 && papers.Count <=0)
        {
            label.SetActive(true);
        }
        else
        {
            label.SetActive(false);
            Debug.Log("Label inactive");
        }
    }

    //public bool isPapers()
    //{        
    //    int paperCount = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPapers();        
    //    bool noPaper = false;        
    //    if (paperCount != papers.Count)
    //    {
    //        noPaper = true;
    //    }
    //    return !noPaper;
    //}

    //public bool isItems()
    //{
    //    List<Item> orderItems = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetItems();
    //    bool isItem = false;
    //    bool noItem = false;        
    //    foreach (Item orderItem in orderItems)
    //    {
    //        if (orderItem.GetName() != "Ecstasy")
    //        {
    //            foreach (Item item in items)
    //            {
    //                if (orderItem.GetName() == item.GetName())
    //                {
    //                    if (orderItem.amount >= item.amount)
    //                        isItem = true;
    //                }
    //            }
    //            if (isItem) continue;
    //            else
    //            {
    //                noItem = true;
    //            }
    //        }
    //    }
    //    int orderAmount = 0;
    //    foreach (Item orderItem in orderItems)
    //    {
    //        if (orderItem.GetName() == "Ecstasy")
    //        {
    //            orderAmount += orderItem.amount;
    //        }
    //    }
    //    int amount = 0;
    //    foreach (Item item in items)
    //    {
    //        if (item.GetName() == "Ecstasy")
    //        {
    //            amount += item.amount;
    //        }
    //    }
    //    if (orderAmount > amount)
    //    {
    //        noItem = true;
    //    }        
    //    if (items.Count == 0 && orderItems.Count != 0) noItem = true;
    //    return !noItem;
    //}

    //public int NoExtraItem()
    //{
    //    List<Item> orderItems = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetItems();
    //    return GetItemsPrice() - GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPrice();
    //}

    //public int EnoughCash()
    //{
    //    return GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPrice() - GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount();
    //}

    //private int GetItemsAmount()
    //{
    //    int amount = 0;
    //    foreach (Item item in items)
    //    {
    //        amount += item.amount;
    //    }
    //    return amount;
    //}

    //public int GetItemsPrice()
    //{
    //    int price = 0;
    //    foreach (Item item in items)
    //    {
    //        price += item.amount * item.GetPrice();
    //    }
    //    Item paper = new Item { itemType = Item.ItemType.Paper, amount = 1 };
    //    price += paper.GetPrice() * papers.Count;
    //    return price;
    //}

    public void DeleteOrderItems()
    {
        DeleteCash();

        foreach (Paper paper in papers)
        {
            Destroy(paper.gameObject);
        }
        foreach (ItemSlot itemSlot in itemSlots)
        {
            Destroy(itemSlot.gameObject);
        }
        itemSlots = new List<ItemSlot>();
        items = new List<Item>();
        papers = new List<Paper>();
    }

    public void DeleteCash()
    {
        GameObject[] cash = GameObject.FindGameObjectsWithTag("Cash");
        foreach (GameObject gm in cash)
        {
            Destroy(gm);
        }
    }
}
