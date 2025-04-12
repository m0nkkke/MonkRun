using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _characterSkinItemPrefab; // ������ ��� ������ ����������

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        if (shopItem == null)
        {
            Debug.LogError("ShopItem is null");
            return null; // ���������� null, ���� ���������� ������ shopItem ������
        }

        ShopItemView instance;
        switch (shopItem)
        {
            case CharacterSkinsItem characterSkinsItem:
                if (_characterSkinItemPrefab == null)
                {
                    Debug.LogError("CharacterSkinItem prefab is not assigned.");
                    return null; // ���� ������ �� ��������, ������� ������ � ���������� null
                }
                instance = Instantiate(_characterSkinItemPrefab, parent); // ������� ��������� �����
                break;

            default:
                Debug.LogError($"����������� ��� �����: {shopItem.GetType().Name}");
                return null; // ���������� null, ���� ��� ����� �� ���������
        }

        instance.Initialize(shopItem); // ������������� �����
        return instance; // ���������� ��������� ��������� �����
    }
}
