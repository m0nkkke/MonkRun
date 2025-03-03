using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpenSkinsChecker : IShopItemVisiter
{
    private IPersistentData _persistentData;
    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinsItem characterSkinsItem)
        => IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinsItem.SkinType);
}
