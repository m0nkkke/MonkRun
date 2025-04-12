using System;
using UnityEngine;

public class Wallet
{
    public event Action<int> CoinsChanged;

    private IPersistentData _persistentData;

    public Wallet(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    // Добавление монет
    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _persistentData.PlayerData.Money += coins;
        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }

    // Получение текущих монет
    public int GetCurrentCoins() => _persistentData.PlayerData.Money;

    // Проверка, хватает ли монет
    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));
        return _persistentData.PlayerData.Money >= coins;
    }

    // Тратим монеты
    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _persistentData.PlayerData.Money -= coins;
        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }
}