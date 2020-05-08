using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
        Debug.Log("Refresh");
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotSellSize = 100f;

        foreach (Item item in inventory.GetItemList())
        {
            
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate { onClick(item); });

            itemSlotRectTransform.anchoredPosition = new Vector3(x * itemSlotSellSize, y * itemSlotSellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI text = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1) { 
            text.SetText(item.amount.ToString());
            }
            else
            {
                text.SetText("");
            }
            x++;
            if (x > 2)
            {
                x = 0;
                y--;
            }
        }
    }

    public void onClick(Item item)
    {
        FindObjectOfType<Sound>().ButtonSound();
        ItemWorld.DropItem(item);
        inventory.RemoveItem(new Item { itemType = item.itemType, amount = 1 });
    }  

}
