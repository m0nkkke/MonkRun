using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedSkinChecker : IShopItemVisiter
{
    private IPersistentData _persistentData;
    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinsItem characterSkinsItem)
        => IsSelected = _persistentData.PlayerData.SelectedCharacterSkins == characterSkinsItem.SkinType;
}
