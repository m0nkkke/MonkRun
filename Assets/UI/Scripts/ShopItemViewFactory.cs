using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
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

    private class ShopItemVisitor : IShopItemVisiter
    {
        private ShopItemView _characterSkinItemPrefab;

        public ShopItemVisitor(ShopItemView characterSkinItemPrefab)
        {
            _characterSkinItemPrefab = characterSkinItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinsItem characterSkinsItem) => Prefab = _characterSkinItemPrefab;
    }
}
