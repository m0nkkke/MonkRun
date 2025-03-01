using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPanel : MonoBehaviour
{
    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();
        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

            spawnedItem.Click += OnItemViewClick;

            spawnedItem.Unselect();
            spawnedItem.UnHighlight();

            _shopItems.Add(spawnedItem);
        }
    }

    private void OnItemViewClick(ShopItemView obj)
    {
        throw new NotImplementedException();
    }

    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}

