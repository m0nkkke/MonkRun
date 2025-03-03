using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnLocker : IShopItemVisiter
{
    private IPersistentData _persistentData;

    public SkinUnLocker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinsItem characterSkinsItem)
        => _persistentData.PlayerData.OpenCharacterSkin(characterSkinsItem.SkinType);
}
