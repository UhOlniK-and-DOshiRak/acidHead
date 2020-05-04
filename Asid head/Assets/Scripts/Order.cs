using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    //public Product[] products;
    public bool wrongName;
    public Cash[] cash;
    [SerializeField] private int cashAmount;
    [SerializeField] private List<Item> items;
    [SerializeField] private int price;
    [SerializeField] private Item paper;

    private void Start()
    {
        price = 0;
        cashAmount = 0;
        foreach (Item item in items)
        {
            price += item.amount * item.GetPrice();
        }
        paper.itemType = Item.ItemType.Paper;
        paper.name = paper.GetName();
        price += paper.GetPrice() * paper.amount;
        foreach (Cash money in cash)
        {
            cashAmount += money.value;
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public int GetPapers()
    {
        return paper.amount;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetCashAmount()
    {
        return cashAmount;
    }
}
