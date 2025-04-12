using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpenSkinsChecker : IShopItemVisiter
{
    private IPersistentData _persistentData;
    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    // Универсальная проверка через Visit для всех типов
    public void Visit(ShopItem shopItem)
    {
        // Явная проверка типа перед вызовом
        if (shopItem is CharacterSkinsItem characterSkinsItem)
        {
            Visit(characterSkinsItem);
        }
        else
        {
            Debug.LogError($"Неверный тип элемента: {shopItem.GetType().Name}. Ожидался тип CharacterSkinsItem.");
        }
    }

    public void Visit(CharacterSkinsItem characterSkinsItem)
    {
        // Проверка, что скин открыт
        if (_persistentData.PlayerData != null && _persistentData.PlayerData.OpenCharacterSkins != null)
        {
            IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinsItem.SkinType);
            Debug.Log($"Проверка скина: {characterSkinsItem.SkinType}. Открыт: {IsOpened}");
        }
        else
        {
            IsOpened = false;
            Debug.Log($"Нет данных игрока или данных о скинах. Скин {characterSkinsItem.SkinType} заблокирован.");
        }
    }
}
