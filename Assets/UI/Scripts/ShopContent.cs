using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<CharacterSkinsItem> _characterSkinsItems;
    
    public IEnumerable<CharacterSkinsItem> CharacterSkins => _characterSkinsItems;

    private void OnValidate()
    {
        var characterSkinsDuplicate = _characterSkinsItems.GroupBy(item => item.SkinType)
            .Where(array =>  array.Count() > 1);

        if (characterSkinsDuplicate.Count() > 0)
            throw new InvalidOperationException(nameof(_characterSkinsItems));
    }
}

