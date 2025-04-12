using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopPanel : MonoBehaviour
{
    public static ShopPanel Instance;  // ��������� Instance

    public event Action<ShopItemView> ItemViewClicked; // ������� ��� ����� �� ����
    private List<ShopItemView> _shopItems = new List<ShopItemView>(); // ������ ������

    [SerializeField] private Transform _itemsParent; // �����, ��� ����� ����������� �����
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory; // ������� ��� �������� ������

    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    // ������������� � ����������
    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }

    private void Awake()
    {
        Instance = this; // ������������� Instance
    }

    // ����� ��� ����������� ������
    public void Show(IEnumerable<ShopItem> items)
    {
        Debug.Log("���������� ����� �� ������...");

        Clear(); // ������� ������ �� ������ ������

        foreach (ShopItem item in items)
        {
            if (item == null)
            {
                Debug.LogError("������ ������ ������� � ������ ������.");
                continue; // ���������� ������ ��������
            }

            if (item is CharacterSkinsItem characterSkinItem)
            {
                ShopItemView spawnedItem = _shopItemViewFactory.Get(characterSkinItem, _itemsParent);
                spawnedItem.Click += OnItemViewClick; // ����������� ������� �����
                spawnedItem.Unselect();  // ������� ��������� �����
                spawnedItem.UnHighlight(); // ������� ��������� �����

                // ���������, ��� item ��������� ���������������
                if (spawnedItem.Item != null)
                {
                    try
                    {
                        // ��������� ����
                        if (spawnedItem.Item is CharacterSkinsItem characterSkinsItemInner)
                        {
                            Debug.Log($"���� {characterSkinsItemInner.SkinType} �����������.");
                            _openSkinsChecker.Visit(characterSkinsItemInner);

                            if (_openSkinsChecker.IsOpened)
                            {
                                spawnedItem.Unlock(); // ������������ ����
                            }
                            else
                            {
                                spawnedItem.Lock(); // ���� ������������
                            }
                        }
                        else
                        {
                            Debug.LogError($"Item � spawnedItem �� �������� ����� CharacterSkinsItem. ���: {spawnedItem.Item.GetType().Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"������ ��� �������� �����: {ex.Message}");
                    }
                }
                else
                {
                    Debug.LogError("Item � spawnedItem ����� null.");
                }

                _shopItems.Add(spawnedItem); // ��������� ���� � ������
            }
        }

        LoadSelectedSkin(); // ��������� ��������� ����
    }

    // ����� ��� ��������� �����
    public void Select(ShopItemView itemView)
    {
        // ������� ��������� � ����������� �����, ���� �� ��� ������
        foreach (var item in _shopItems)
        {
            if (item != itemView)
            {
                item.Unselect(); // ���������� ����� � ������ ������
            }
        }

        itemView.ToggleSelection(_shopItems); // ����������� ��������� ������� � ����
        SaveSelectedSkin(itemView); // ��������� ��������� ����
    }

    // ����� ��� ��������� ������ �� ������
    private void OnItemViewClick(ShopItemView itemView)
    {
        Select(itemView); // �������� ��������� �����
        ItemViewClicked?.Invoke(itemView); // �������� ������� ��� ���������� �����
    }

    // ���������� ���������� �����
    private void SaveSelectedSkin(ShopItemView itemView)
    {
        if (itemView.Item is CharacterSkinsItem characterSkinItem)
        {
            // ��������� ��� ����� � PlayerPrefs
            PlayerPrefs.SetString("SelectedSkin", characterSkinItem.SkinType.ToString());
            PlayerPrefs.Save();
        }
    }

    // �������� ��������� ���������� �����
    private void LoadSelectedSkin()
    {
        // �������� ��� ���������� ����� �� PlayerPrefs
        string selectedSkinType = PlayerPrefs.GetString("SelectedSkin", string.Empty);

        if (string.IsNullOrEmpty(selectedSkinType))
        {
            // ���� ������������ ��������� ���, �������� BaseMonk �� ���������
            SetDefaultSkin();
        }
        else
        {
            foreach (var item in _shopItems)
            {
                if (item.Item is CharacterSkinsItem characterSkinItem && characterSkinItem.SkinType.ToString() == selectedSkinType)
                {
                    item.ToggleSelection(_shopItems); // �������� ����������� ����
                    break;
                }
            }
        }
    }

    // ������������� BaseMonk �� ���������
    private void SetDefaultSkin()
    {
        foreach (var item in _shopItems)
        {
            if (item.Item is CharacterSkinsItem characterSkinItem && characterSkinItem.SkinType == CharacterSkins.BaseMonk)
            {
                item.ToggleSelection(_shopItems); // �������� BaseMonk �� ���������
                SaveSelectedSkin(item); // ��������� ��������� ����
                break;
            }
        }
    }

    // ������� ������ ������
    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick; // ������� ���������� �����
            Destroy(item.gameObject); // ������� ������ ����
        }

        _shopItems.Clear(); // ������� ������ ������
    }

    public List<ShopItemView> GetShopItems()
    {
        return _shopItems;
    }
}
