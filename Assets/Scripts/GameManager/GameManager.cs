using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int MaxScore = 0;
    public int MaxBananas = 0;
    public int AllBananas = 0;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int roadSpeed = 8;
    public int Score = 0;
    public int Bananas = 0;
    public bool isRunning = true;

    public int CountMaxBananas = 0;

    private const string filename = "result.json";
    private const int START_SPEED = 8;

    private const string KEY_SAVE = "mainData";

    public GameData gameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameData = SaveManager.Load<GameData>(KEY_SAVE);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //if (isRunning)
        //{
        //    roadSpeed = roadSpeed + Score / (roadSpeed * 3);
        //}

    }
    public void ResetGame()
    {
        try
        {
            //SaveResult();
            Save();
        }
        catch { }
        Instance.Score = 0;
        Instance.Bananas = 0;
        isRunning = false;

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
    public void Save()
    {
        gameData.MaxScore = Math.Max(gameData.MaxScore, Instance.Score);
        gameData.MaxBananas = Math.Max(gameData.MaxBananas, Instance.Bananas);
        gameData.AllBananas += Instance.Bananas;
        SaveManager.Save(KEY_SAVE, gameData);
    }
    public void Load() 
    {

    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneIndex);
        Bananas = 0;
        Score = 0;
        roadSpeed = START_SPEED;
        CountMaxBananas = 0;
        isRunning = true;
    }

    public void UpdateSpeed()
    {
        // Максимум скорость увеличится на 2 за 
        int stepSpeedIncrease = 1;
        roadSpeed = roadSpeed + stepSpeedIncrease;
        // + Bananas / CountMaxBananas
    }

    public readonly List<int> StepsSpeedIncrease = new List<int>() { 20, 50, 100, 200, 400, 900, 1300, 1700};
    public void IncreaseScore()
    {
        Score += 1;
        if (StepsSpeedIncrease.Contains(Score))
        {
            UpdateSpeed();
        }
    }


}
