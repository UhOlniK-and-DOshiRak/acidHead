using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheck : MonoBehaviour
{
    List<Item> orderItems;
    List<Item> slotItems;
    int papers;
    Workspace workspace;
    Player player;
    DialogManager dialogManager;
    public Warning warning;

    void Start()
    {
        workspace = FindObjectOfType<Workspace>();
        player = FindObjectOfType<Player>();
        dialogManager = FindObjectOfType<DialogManager>();
    }

    public void CheckOnSale()
    {
        int errors = 0;
        int fine = 0;
        orderItems = FindObjectOfType<Client>().GetComponent<Order>().GetItems();
        slotItems = FindObjectOfType<OrderSlot>().GetItems();
        papers = FindObjectOfType<OrderSlot>().GetPapersCount();
        if (!DialogManager.isMoney)
        {
            fine += GetItemsPrice();
            errors += 1;
            InstantiateWarning(Warning.WarningType.DoNotGetMoney);
        }
        else
        {
            player.AddMoney(GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount());
            if (NoExtraItem() <= 0)
            {
                if (EnoughCash() > 0)
                {
                    errors += 1;
                    fine += EnoughCash();
                    InstantiateWarning(Warning.WarningType.NotEnoughMoney);
                }
            }
            else if (NoExtraItem() > 0)
            {
                errors += 1;
                fine += NoExtraItem();
                InstantiateWarning(Warning.WarningType.ExtraItem);
            }

            if (!isItems() || !isPapers())
            {
                errors += 1;
                InstantiateWarning(Warning.WarningType.NoItem);
            }

        }

        if (DataHolder.nameCheckEnabled)
        {
            if (FindObjectOfType<Client>().GetComponent<Order>().wrongName)
            {
                errors += 1;
                InstantiateWarning(Warning.WarningType.WrongName);
            }
        }

        if (DataHolder.ageCheckEnabled)
        {
            if (FindObjectOfType<Client>().type == Client.ClientType.Child)
            {
                errors += 1;
                InstantiateWarning(Warning.WarningType.WrongAge);
            }
        }

        if (DataHolder.sumCheckEnabled)
        {
            if (FindObjectOfType<Client>().GetComponent<Order>().GetPrice() > DataHolder.maxSum)
            {
                errors += 1;
                InstantiateWarning(Warning.WarningType.WrongSum);
            }
        }

        if (fine != 0)
        {
            player.ReduceMoney(fine);
        }

        if (errors != 0)
        {
            player.ReduceAuthority(errors * 10);
        }

        if (fine == 0 && errors == 0)
        {
            player.AddAuthority(5);
            Debug.Log("CorrectOrder");
        }
    }

    public void CheckOnRefuse()
    {
        int errors = 0;
        int fine = 0;
        orderItems = FindObjectOfType<Client>().GetComponent<Order>().GetItems();
        slotItems = FindObjectOfType<OrderSlot>().GetItems();

        if (!DialogManager.isMoney) {
            if (DataHolder.nameCheckEnabled)
            {
                if (FindObjectOfType<Client>().GetComponent<Order>().wrongName) {
                    errors += 1;
                }
            }
            if (DataHolder.ageCheckEnabled)
            {
                if (FindObjectOfType<Client>().type == Client.ClientType.Child)
                {
                    errors += 1;
                }
            }
            if (DataHolder.sumCheckEnabled)
            {
                if (FindObjectOfType<Client>().GetComponent<Order>().GetPrice() > DataHolder.maxSum)
                {
                    errors += 1;
                }
            }
            if (EnoughCash() > 0)
            {
                errors += 1;
            }
            if (errors != 0)
            {
                player.AddAuthority(5);
            }
            else
            {
                player.ReduceAuthority(10);
                InstantiateWarning(Warning.WarningType.NoReasonRefuse);
            }
        }
        else
        {
            player.AddMoney(GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount());
            player.ReduceAuthority(10);
            InstantiateWarning(Warning.WarningType.GetMoneyRefuse);
            FindObjectOfType<OrderSlot>().DeleteCash();
        }
    }

    public void InstantiateWarning(Warning.WarningType type)
    {
        RectTransform newWarning = Instantiate(warning, GameObject.Find("WarningPosition").GetComponent<Transform>()).GetComponent<RectTransform>();
        newWarning.GetComponent<Warning>().SetText(type);
        FindObjectOfType<Sound>().WarningSound();
    }

    public int GetItemsPrice()
    {
        int price = 0;
        foreach (Item item in slotItems)
        {
            price += item.amount * item.GetPrice();
        }
        Item paper = new Item { itemType = Item.ItemType.Paper, amount = 1 };
        price += paper.GetPrice() * papers;
        return price;
    }

    public int NoExtraItem()
    {
        List<Item> orderItems = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetItems();
        return GetItemsPrice() - GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPrice();
    }

    public int EnoughCash()
    {
        return GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPrice() - GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount();
    }

    private int GetItemsAmount()
    {
        int amount = 0;
        foreach (Item item in slotItems)
        {
            amount += item.amount;
        }
        return amount;
    }

    public bool isItems()
    {
        bool isItem = false;
        bool noItem = false;
        foreach (Item orderItem in orderItems)
        {
            if (orderItem.GetName() != "Ecstasy")
            {
                foreach (Item item in slotItems)
                {
                    if (orderItem.GetName() == item.GetName())
                    {
                        if (orderItem.amount >= item.amount)
                            isItem = true;
                    }
                }
                if (isItem) continue;
                else
                {
                    noItem = true;
                }
            }
        }
        int orderAmount = 0;
        foreach (Item orderItem in orderItems)
        {
            if (orderItem.GetName() == "Ecstasy")
            {
                orderAmount += orderItem.amount;
            }
        }
        int amount = 0;
        foreach (Item item in slotItems)
        {
            if (item.GetName() == "Ecstasy")
            {
                amount += item.amount;
            }
        }
        if (orderAmount > amount)
        {
            noItem = true;
        }
        if (slotItems.Count == 0 && orderItems.Count != 0) noItem = true;
        return !noItem;
    }

    public bool isPapers()
    {
        int paperCount = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPapers();
        bool noPaper = false;
        if (paperCount != papers)
        {
            noPaper = true;
        }
        return !noPaper;
    }
}
