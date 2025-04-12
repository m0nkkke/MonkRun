using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkinItem", menuName = "Shop/CharacterSkins")]
public class CharacterSkinsItem : ShopItem
{
    [field: SerializeField] public CharacterSkins SkinType { get; private set; }
}
