using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int MaxScore;
    public int MaxBananas;
    public int AllBananas;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int roadSpeed = 8;
    public int Score = 0;
    public int Bananas = 0;

    private readonly string filename = "result.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetGame()
    {
        SaveResult();
        Instance.Score = 0;
        Instance.Bananas = 0;
    }

    public void SaveResult()
    {
        GameData data;

        // Проверяем, существует ли файл
        if (File.Exists(filename))
        {
            // Если файл существует, считываем данные из него
            string json = File.ReadAllText(filename);
            data = JsonUtility.FromJson<GameData>(json);
            print(data);
        }
        else
        {
            // Если файл не существует, создаем новый объект GameData
            data = new GameData
            {
                MaxScore = 0,
                MaxBananas = 0,
                AllBananas = 0
            };
        }

        // Обновляем значения
        data.MaxScore = Math.Max(data.MaxScore, Instance.Score);
        data.MaxBananas = Math.Max(data.MaxBananas, Instance.Bananas);
        data.AllBananas += Instance.Bananas;

        // Сохраняем обновленные данные в файл
        string updatedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(filename, updatedJson);
    }

}
