using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<CharacterSkinsItem> _characterSkinsItems;

    public IEnumerable<CharacterSkinsItem> CharacterSkinsItems => _characterSkinsItems;

    private void OnValidate()
    {
        if (_characterSkinsItems == null || !_characterSkinsItems.Any())
        {
            Debug.LogWarning("No skins found in ShopContent!");  // Логируем предупреждение, если скинов нет
            return;
        }

        var characterSkinsDuplicate = _characterSkinsItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (characterSkinsDuplicate.Count() > 0)
        {
            Debug.LogError("Duplicate skins found!");  // Логируем ошибку, если есть дубликаты
            throw new InvalidOperationException(nameof(_characterSkinsItems));
        }
    }
}

