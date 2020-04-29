using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private Transform itemWorldContainer;
    public GameObject[] worldPosTabacco;

    public static ItemWorld SpawnItemWorld(Item item)
    {
        Transform transform = ItemAssets.Instance.itemWorld;
        Transform itemContainer = GameObject.Find("ItemWorldContainer").transform;
        RectTransform itemContainerRectTransform = Instantiate(ItemAssets.Instance.itemWorld, itemContainer).GetComponent<RectTransform>();        
        itemContainerRectTransform.gameObject.SetActive(true);
        GameObject[] positions = GameObject.FindGameObjectsWithTag("ObjectPositions");
        RectTransform rectTransformPosition = positions[Random.Range(0, positions.Length)].GetComponent<RectTransform>();
        itemContainerRectTransform.anchoredPosition = rectTransformPosition.anchoredPosition;
        ItemWorld itemWorld = itemContainerRectTransform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        Debug.Log("ItemWorldInstantiate");

        return itemWorld;
    }

    public static ItemWorld SpawnItemTabacco()
    {
        Transform transform = ItemAssets.Instance.itemWorld;
        Transform itemContainer = GameObject.Find("ItemWorldContainer").transform;
        RectTransform itemContainerRectTransform = Instantiate(ItemAssets.Instance.itemWorld, itemContainer).GetComponent<RectTransform>();
        itemContainerRectTransform.gameObject.SetActive(true);
        GameObject[] positions = GameObject.FindGameObjectsWithTag("PosTabacco");
        RectTransform rectTransformPosition = positions[Random.Range(0, positions.Length)].GetComponent<RectTransform>();
        itemContainerRectTransform.anchoredPosition = rectTransformPosition.anchoredPosition;
        ItemWorld itemWorld = itemContainerRectTransform.GetComponent<ItemWorld>();
        itemWorld.SetItem(new Item { itemType = Item.ItemType.Tabacco, amount = 1 });
        Debug.Log("ItemWorldInstantiate");

        return itemWorld;
    }

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        worldPosTabacco = GameObject.FindGameObjectsWithTag("PosTabacco");
    }

    public void SetItem(Item item)
    {
        this.item = item;
        image.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static ItemWorld DropItem(Item item)
    {
        ItemWorld itemWorld = SpawnItemWorld(new Item { itemType = item.itemType, amount = 1 });
        return itemWorld;
    }

    public static ItemWorld DropTabacco()
    {
        ItemWorld itemWorld = SpawnItemTabacco();
        return itemWorld;
    }
}
