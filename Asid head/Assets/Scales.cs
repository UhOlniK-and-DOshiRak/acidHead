using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scales : MonoBehaviour
{
    private Dropdown dropdown;
    List<string> itemNames;
    List<Item> items;
    Item currentItem;
    int currentPrice, priceForAll, currentCount;
    Text weight, price, totalPrice, count;
    Image image;

    void Start()
    {        
        itemNames = new List<string>();
        items = new List<Item>();
        List<Item> playerItems = FindObjectOfType<Player>().items;
        bool extasyCounted = false;
        bool firstItemFound = false;
        foreach (Item playerItem in playerItems)
        {
            if (playerItem.GetName() != "Ecstasy")
            {
                itemNames.Add(playerItem.GetRussianName());
                items.Add(playerItem);
                if (!firstItemFound)
                {
                    currentItem = playerItem;
                    firstItemFound = true;
                }
            }
            else if (!extasyCounted)
            {
                itemNames.Add(playerItem.GetRussianName());
                items.Add(playerItem);
                extasyCounted = true;
            }
        }
        Item paper = new Item { itemType = Item.ItemType.Paper, amount = 1 };
        items.Add(paper);
        itemNames.Add(paper.GetRussianName());
        dropdown = FindObjectOfType<Dropdown>().GetComponent<Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(itemNames);
        weight = transform.Find("Weight").GetComponent<Text>();
        price = transform.Find("Price").GetComponent<Text>();
        totalPrice = transform.Find("TotalPriceImage").Find("TotalPrice").GetComponent<Text>();
        image = transform.Find("Sprite").GetComponent<Image>();
        count = transform.Find("Quantity").GetComponent<Text>();
        currentCount = 1;
        count.text = currentCount.ToString();
        priceForAll = 0;
        totalPrice.text = priceForAll.ToString();
        image.sprite = currentItem.GetSprite();
        weight.text = "Вес: " + currentItem.GetWeight().ToString()+"мг";
        price.text = "Цена: " + currentItem.GetPrice().ToString();
        currentPrice = currentItem.GetPrice();
    }

    public void ChangeItem()
    {
        FindObjectOfType<Sound>().ButtonSound();
        currentCount = 1;
        count.text = currentCount.ToString();
        currentItem = items[dropdown.value];
        price.text = "Цена: " + currentItem.GetPrice().ToString();
        if (currentItem.itemType != Item.ItemType.Paper)
        weight.text = "Вес: " + currentItem.GetWeight().ToString() + "мг";
        else weight.text = "---";
        currentPrice =  currentItem.GetPrice();
        image.sprite =  currentItem.GetSprite();
    }

    public void Increase()
    {
        FindObjectOfType<Sound>().ButtonSound();
        currentCount += 1;
        count.text = currentCount.ToString();
        currentPrice += currentItem.GetPrice();
        price.text = "Цена: " + currentPrice.ToString();
        if (currentItem.itemType != Item.ItemType.Paper)
            weight.text = "Вес: " + (currentItem.GetWeight() * currentCount).ToString() + "мг";
        else weight.text = "---";
    }

    public void Decrease()
    {
        FindObjectOfType<Sound>().ButtonSound();
        if (currentCount > 1)
        {
            currentCount -= 1;
            count.text = currentCount.ToString();
            currentPrice -= currentItem.GetPrice();
            price.text = "Цена: " + currentPrice.ToString();
            if (currentItem.itemType != Item.ItemType.Paper)
                weight.text = "Вес: " + (currentItem.GetWeight() * currentCount).ToString() + "мг";
            else weight.text = "---";
        }
    }

    public void Add()
    {
        FindObjectOfType<Sound>().ButtonSound();
        priceForAll += currentPrice;
        totalPrice.text = priceForAll.ToString();
        currentCount = 1;
        count.text = currentCount.ToString();
        currentPrice = currentItem.GetPrice();
        price.text = "Цена: " + currentPrice.ToString();
        if (currentItem.itemType != Item.ItemType.Paper)
            weight.text = "Вес: " + currentItem.GetWeight().ToString() + "мг";
        else weight.text = "---";
    }

    public void Clear()
    {
        FindObjectOfType<Sound>().ButtonSound();
        priceForAll = 0;
        totalPrice.text = priceForAll.ToString();
        dropdown.value = 0;
    }


}
