using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SkinSelector : IShopItemVisiter
{
    private IPersistentData _persistentData;

    public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinsItem characterSkinsItem)
        => _persistentData.PlayerData.SelectedCharacterSkins = characterSkinsItem.SkinType;
}
