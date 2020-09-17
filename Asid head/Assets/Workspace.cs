using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workspace : MonoBehaviour
{
    public ItemSlot package;
    public Paper paper;
    bool packageInstantiated;
    public static bool paperInstantiated;
    public Warning warning;
    public GameObject book;

    void Start()
    {
        packageInstantiated = false;
        paperInstantiated = false;
    }

    public void GetPackage()
    {
        //if (!packageInstantiated)
        //{
            RectTransform rectTransform = Instantiate(package, GameObject.Find("ItemWorldContainer").transform).GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GameObject.Find("PosPackage").GetComponent<RectTransform>().anchoredPosition;
            packageInstantiated = true;
        FindObjectOfType<Sound>().ButtonSound();
            //rectTransform.anchoredPosition = GameObject.Find("PosCash").GetComponent<RectTransform>().anchoredPosition;
        //}
        //else
        //{
        //    //RemovePackage();
        //}
    }

    //void RemovePackage()
    //{
    //    GameObject itemSlot = GameObject.FindGameObjectWithTag("Package");
    //    if (itemSlot != null) {
    //        Destroy(itemSlot);
    //        packageInstantiated = false;
    //    }
    //}

    public void GetPaper()
    {
        //if (!paperInstantiated)
        //{
            RectTransform rectTransform = Instantiate(paper, GameObject.Find("ItemWorldContainer").transform).GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GameObject.Find("PosPaper").GetComponent<RectTransform>().anchoredPosition;
        rectTransform.SetAsFirstSibling();
            paperInstantiated = true;
            FindObjectOfType<Sound>().ButtonSound();
        //}
    }

    public void GetTabacco()
    {
        //if (paperInstantiated)
        //{
            ItemWorld.DropTabacco();
        //}
    }

    public void ClearOrderSlot()
    {
        GameObject.Find("OrderContainer").GetComponent<OrderSlot>().ClearSlot();
        FindObjectOfType<Sound>().ButtonSound();
    }

    public void Sale()
    {
        
        FindObjectOfType<Sound>().ButtonSound();
        OrderSlot orderSlot = GameObject.Find("OrderContainer").GetComponent<OrderSlot>();        
        FindObjectOfType<OrderCheck>().CheckOnSale();        
        orderSlot.DeleteOrderItems();
        FindObjectOfType<DialogManager>().Sale();
        FindObjectOfType<OrderSlot>().SetLabel();
        FindObjectOfType<CashCount>().Clear();
    }

    //public void InstantiateWarning(Warning.WarningType type)
    //{
    //    RectTransform newWarning = Instantiate(warning, GameObject.Find("WarningPosition").GetComponent<Transform>()).GetComponent<RectTransform>();
    //    newWarning.GetComponent<Warning>().SetText(type);
    //    FindObjectOfType<Sound>().WarningSound();
    //}

    public void OpenBook()
    {
        book.SetActive(true);
        FindObjectOfType<Sound>().BookSound();
    }

    public bool checkName(Order order, Player player)
    {
        if (order.wrongName) {            
            return false;
        }
        return true;
    }

    public Order getOrder()
    {
        return GameObject.FindGameObjectWithTag("Client").GetComponent<Order>();
    }

    public Player GetPlayer()
    {
        return FindObjectOfType<Player>();
    }

    public Client GetClient()
    {
        return GameObject.FindGameObjectWithTag("Client").GetComponent<Client>();
    }


}
