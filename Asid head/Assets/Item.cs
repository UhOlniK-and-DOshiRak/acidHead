using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public enum ItemType
    {
        Methamphetamine,
        Ecstasy1,
        Ecstasy2,
        Ecstasy3,
        Ecstasy4,
        Hashish,
        Methadone,
        Morphine,
        Marijuana,
        Tabacco,
        Paper
    }

    public ItemType itemType;
    public int amount;
    public string name;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Methamphetamine: return ItemAssets.Instance.methamphetamineSprite;
            case ItemType.Methadone: return ItemAssets.Instance.methadoneSprite;
            case ItemType.Ecstasy1: return ItemAssets.Instance.ecstasySprite[0];
            case ItemType.Ecstasy2: return ItemAssets.Instance.ecstasySprite[1];
            case ItemType.Ecstasy3: return ItemAssets.Instance.ecstasySprite[2];
            case ItemType.Ecstasy4: return ItemAssets.Instance.ecstasySprite[3];
            case ItemType.Hashish: return ItemAssets.Instance.hashishSprite;
            case ItemType.Morphine: return ItemAssets.Instance.morphineSprite;
            case ItemType.Marijuana: return ItemAssets.Instance.marijuanaSprite;
            case ItemType.Tabacco: return ItemAssets.Instance.tabaccoSprite;
            case ItemType.Paper: return ItemAssets.Instance.paperSprite;
        }
    }

    public int GetWeight()
    {
        switch (itemType)
        {
            default:
            case ItemType.Methamphetamine: return 20;
            case ItemType.Methadone: return 20;
            case ItemType.Ecstasy1: return 15;
            case ItemType.Ecstasy2: return 15;
            case ItemType.Ecstasy3: return 15;
            case ItemType.Ecstasy4: return 15;
            case ItemType.Hashish: return 30;
            case ItemType.Morphine: return 20;
            case ItemType.Marijuana: return 25;
        }
    }

    public int GetPrice()
    {
        switch (itemType)
        {
            default:
            case ItemType.Methamphetamine: return 15;
            case ItemType.Methadone: return 10;
            case ItemType.Ecstasy1: return 5;
            case ItemType.Ecstasy2: return 5;
            case ItemType.Ecstasy3: return 5;
            case ItemType.Ecstasy4: return 5;
            case ItemType.Hashish: return 7;
            case ItemType.Morphine: return 20;
            case ItemType.Marijuana: return 5;
            case ItemType.Paper: return 25;
        }
    }

    public string GetName()
    {
        switch (itemType)
        {
            default:
            case ItemType.Methamphetamine: return "Methamphetamine";
            case ItemType.Methadone: return "Methadone";
            case ItemType.Ecstasy1: return "Ecstasy";
            case ItemType.Ecstasy2: return "Ecstasy";
            case ItemType.Ecstasy3: return "Ecstasy";
            case ItemType.Ecstasy4: return "Ecstasy";
            case ItemType.Hashish: return "Hashish";
            case ItemType.Morphine: return "Morphine";
            case ItemType.Marijuana: return "Marijuana";
            case ItemType.Tabacco: return "Tabacco";
            case ItemType.Paper: return "Paper";
        }
    }

    public string GetRussianName()
    {
        switch (itemType)
        {
            default:
            case ItemType.Methamphetamine: return "Метамфетамин";
            case ItemType.Methadone: return "Метадон";
            case ItemType.Ecstasy1: return "Экстази";
            case ItemType.Ecstasy2: return "Экстази";
            case ItemType.Ecstasy3: return "Экстази";
            case ItemType.Ecstasy4: return "Экстази";
            case ItemType.Hashish: return "Гашиш";
            case ItemType.Morphine: return "Морфин";
            case ItemType.Marijuana: return "Марихуана";
            case ItemType.Tabacco: return "Tabacco";
            case ItemType.Paper: return "Косяк";
        }
    }

    public string[] GetPseudonym()
    {
        switch (itemType)
        {
            default:
            case ItemType.Methamphetamine: return new string[] { "Лёд", "Мет", "Меф" };
            //case ItemType.Methadone: return "Methadone";
            case ItemType.Ecstasy1: return new string[] { "Ешка", "Экстази", "Диск" };
            case ItemType.Ecstasy2: return new string[] { "Ешка", "Экстази", "Диск" }; 
            case ItemType.Ecstasy3: return new string[] { "Ешка", "Экстази", "Диск" }; 
            case ItemType.Ecstasy4: return new string[] { "Ешка", "Экстази", "Диск" }; 
            //case ItemType.Hashish: return "Hashish";
            //case ItemType.Morphine: return "Morphine";
            //case ItemType.Marijuana: return "Marijuana";
        }
    }
}
