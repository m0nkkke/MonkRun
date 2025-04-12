using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _characterSkinItemPrefab; // Префаб для скинов персонажей

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        if (shopItem == null)
        {
            Debug.LogError("ShopItem is null");
            return null; // Возвращаем null, если переданный объект shopItem пустой
        }

        ShopItemView instance;
        switch (shopItem)
        {
            case CharacterSkinsItem characterSkinsItem:
                if (_characterSkinItemPrefab == null)
                {
                    Debug.LogError("CharacterSkinItem prefab is not assigned.");
                    return null; // Если префаб не назначен, выводим ошибку и возвращаем null
                }
                instance = Instantiate(_characterSkinItemPrefab, parent); // Создаем экземпляр скина
                break;

            default:
                Debug.LogError($"Неизвестный тип скина: {shopItem.GetType().Name}");
                return null; // Возвращаем null, если тип скина не распознан
        }

        instance.Initialize(shopItem); // Инициализация скина
        return instance; // Возвращаем созданный экземпляр скина
    }
}
