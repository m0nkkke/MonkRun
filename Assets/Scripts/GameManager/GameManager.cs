using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isRunning = true;

    public int CountMaxBananas = 0;

    private readonly string filename = "result.json";
    private readonly int START_SPEED = 8;

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
            SaveResult();
        }
        catch { }
        Instance.Score = 0;
        Instance.Bananas = 0;
        isRunning = false;

    }

    public void SaveResult()
    {
        GameData data;

        // ���������, ���������� �� ����
        if (File.Exists(filename))
        {
            // ���� ���� ����������, ��������� ������ �� ����
            string json = File.ReadAllText(filename);
            data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            // ���� ���� �� ����������, ������� ����� ������ GameData
            data = new GameData
            {
                MaxScore = 0,
                MaxBananas = 0,
                AllBananas = 0
            };
        }

        // ��������� ��������
        data.MaxScore = Math.Max(data.MaxScore, Instance.Score);
        data.MaxBananas = Math.Max(data.MaxBananas, Instance.Bananas);
        data.AllBananas += Instance.Bananas;

        // ��������� ����������� ������ � ����
        string updatedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(filename, updatedJson);
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        // �������� ������ ������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ������������� ������� �����
        SceneManager.LoadScene(currentSceneIndex);
        Bananas = 0;
        Score = 0;
        roadSpeed = START_SPEED;
        CountMaxBananas = 0;
        isRunning = true;
    }

    public void UpdateSpeed()
    {
        // �������� �������� ���������� �� 2 �� 
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
