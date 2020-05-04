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
        if (!paperInstantiated)
        {
            RectTransform rectTransform = Instantiate(paper, GameObject.Find("ItemWorldContainer").transform).GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GameObject.Find("PosPaper").GetComponent<RectTransform>().anchoredPosition;
            paperInstantiated = true;
        }
    }

    public void GetTabacco()
    {
        if (paperInstantiated)
        {
            ItemWorld.DropTabacco();
        }
    }

    public void ClearOrderSlot()
    {
        GameObject.Find("OrderContainer").GetComponent<OrderSlot>().ClearSlot();
    }

    public void Sale()
    {
        Player player = FindObjectOfType<Player>();
        OrderSlot orderSlot = GameObject.Find("OrderContainer").GetComponent<OrderSlot>();
        int orderPrice = orderSlot.GetItemsPrice();
        //if (!orderSlot.isItems())
        //{
        //    Debug.Log("No items");
        //}
        //if (!orderSlot.isPapers())
        //{
        //    Debug.Log("No papers");
        //}
        //if (orderSlot.NoExtraItem() < 0)
        //{
        //    Debug.Log("Get " + Mathf.Abs(orderSlot.NoExtraItem()));
        //}
        //else if (orderSlot.NoExtraItem() > 0)
        //{
        //    Debug.Log("Loss " + orderSlot.NoExtraItem());
        //    player.ReduceMoney(orderSlot.NoExtraItem());
        //}
        //if (orderSlot.EnoughCash() > 0)
        //{
        //    Debug.Log("Need "+ orderSlot.EnoughCash() + " more money");
        //    player.ReduceMoney(orderSlot.EnoughCash());
        //}
        if (!DialogManager.isMoney)
        {
            Debug.Log("Forgot to take Money!");
            InstantiateWarning(Warning.WarningType.DoNotGetMoney);
            player.ReduceMoney(orderSlot.GetItemsPrice());
        }
        else
        {
            player.AddMoney(GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount());
            if (!orderSlot.isItems() || !orderSlot.isPapers())
            {
                InstantiateWarning(Warning.WarningType.NoItem);
                player.ReduceAuthority(5);
            }
            if (orderSlot.NoExtraItem() > 0)
            {
                player.ReduceMoney(orderSlot.NoExtraItem());
                InstantiateWarning(Warning.WarningType.ExtraItem);
            }
            else if (orderSlot.EnoughCash() > 0)
            {
                InstantiateWarning(Warning.WarningType.NotEnoughMoney);
                player.ReduceMoney(orderSlot.EnoughCash());
            }
        }
        if (orderSlot.isItems() && orderSlot.isPapers() && orderSlot.NoExtraItem() == 0 && orderSlot.EnoughCash() == 0 && DialogManager.isMoney && checkLimits(GameObject.FindGameObjectWithTag("Client").GetComponent<Order>(), player))
        {
            Debug.Log("Correct order");
            FindObjectOfType<Player>().AddAuthority(5);            
        }
        
        orderSlot.DeleteOrderItems();
        FindObjectOfType<DialogManager>().Sale();
    }

    public void InstantiateWarning(Warning.WarningType type)
    {
        RectTransform newWarning = Instantiate(warning, GameObject.Find("WarningPosition").GetComponent<Transform>()).GetComponent<RectTransform>();
        newWarning.GetComponent<Warning>().SetText(type);
    }

    public void OpenBook()
    {
        book.SetActive(true);
    }

    bool checkLimits(Order order, Player player)
    {
        if (checkName(order, player)) return true;
        return false;
    }

    bool checkName(Order order, Player player)
    {
        if (order.wrongName) {
            InstantiateWarning(Warning.WarningType.WrongName);
            player.ReduceAuthority(5);
            return false;
        }
        return true;
    }


}
