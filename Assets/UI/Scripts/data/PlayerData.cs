using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private CharacterSkins _selectCharacterSkins;

    private List<CharacterSkins> _openCharacterSkins;

    private int _money;

    public PlayerData()
    {
        _money = 10000;

        _selectCharacterSkins = CharacterSkins.BaseMonk;

        _openCharacterSkins = new List<CharacterSkins>() { _selectCharacterSkins};
    }

    [JsonConstructor]
    public PlayerData(int money, CharacterSkins selectCharacterSkins, List<CharacterSkins> openCharacterSkins)
    {
        Money = money;

        _selectCharacterSkins = selectCharacterSkins;
        _openCharacterSkins = openCharacterSkins;
    }


    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            _money = value;
        }
    }

    public CharacterSkins SelectedCharacterSkins
    {
        get => _selectCharacterSkins;
        set
        {
            if (_openCharacterSkins.Contains(value) == false)
                throw new ArgumentOutOfRangeException(nameof(value));

            _selectCharacterSkins = value;
        }
    }

    public IEnumerable<CharacterSkins> OpenCharacterSkins => _openCharacterSkins;

    public void OpenCharacterSkin(CharacterSkins skin)
    {
        if (_openCharacterSkins.Contains(skin))
            throw new ArgumentOutOfRangeException(nameof(skin));
        _openCharacterSkins.Add(skin);
    }
}
