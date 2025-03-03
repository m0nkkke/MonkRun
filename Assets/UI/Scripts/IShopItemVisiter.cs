using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItemVisiter
{
    void Visit(ShopItem shopItem);

    void Visit(CharacterSkinsItem characterSkinsItem);
}
