using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpenSkinsChecker : IShopItemVisiter
{
    private IPersistentData _persistentData;
    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    // ������������� �������� ����� Visit ��� ���� �����
    public void Visit(ShopItem shopItem)
    {
        // ����� �������� ���� ����� �������
        if (shopItem is CharacterSkinsItem characterSkinsItem)
        {
            Visit(characterSkinsItem);
        }
        else
        {
            Debug.LogError($"�������� ��� ��������: {shopItem.GetType().Name}. �������� ��� CharacterSkinsItem.");
        }
    }

    public void Visit(CharacterSkinsItem characterSkinsItem)
    {
        // ��������, ��� ���� ������
        if (_persistentData.PlayerData != null && _persistentData.PlayerData.OpenCharacterSkins != null)
        {
            IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinsItem.SkinType);
            Debug.Log($"�������� �����: {characterSkinsItem.SkinType}. ������: {IsOpened}");
        }
        else
        {
            IsOpened = false;
            Debug.Log($"��� ������ ������ ��� ������ � ������. ���� {characterSkinsItem.SkinType} ������������.");
        }
    }
}
