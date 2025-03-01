using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _characterSkinItemPrefab;
    
    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemView instance;
        switch (shopItem)
        {
            case CharacterSkinsItem characterSkinsItem:
                instance = Instantiate(_characterSkinItemPrefab, parent);
                break;
            default:
                throw new ArgumentException(nameof(shopItem));
        }
        instance.Initialize(shopItem);
        return instance;
    }
}
