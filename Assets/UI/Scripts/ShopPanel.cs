using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;
    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();
        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

            spawnedItem.Click += OnItemViewClick;

            spawnedItem.Unselect();
            spawnedItem.UnHighlight();

            _openSkinsChecker.Visit(spawnedItem.Item);
            if (_openSkinsChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItem.Item);

                if (_selectedSkinChecker.IsSelected)
                {
                    spawnedItem.Select();
                    spawnedItem.Highlight();
                    ItemViewClicked?.Invoke(spawnedItem);
                }
                spawnedItem.UnLock();
            }
            else
            {
                spawnedItem.Lock();
            }


            _shopItems.Add(spawnedItem);
        }
    }

    private void OnItemViewClick(ShopItemView itemView)
    {
        Highlight(itemView);
        ItemViewClicked?.Invoke(itemView);
    }

    private void Highlight(ShopItemView shopItemView)
    {
        foreach (var item in _shopItems)
            item.UnHighlight();
        
        shopItemView.Highlight();
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

