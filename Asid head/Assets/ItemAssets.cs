using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite methamphetamineSprite;
    public Sprite[] ecstasySprite;
    public Sprite hashishSprite;
    public Sprite methadoneSprite;
    public Sprite morphineSprite;
    public Sprite marijuanaSprite;
    public Sprite tabaccoSprite;
    public Sprite paperSprite;

    public RectTransform itemWorld;

}
