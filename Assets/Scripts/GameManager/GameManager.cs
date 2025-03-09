using System;
using System.Collections;
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
    public GameObject menuLose;
    private CanvasGroup menuLoseCanvasGroup;

    public int roadSpeed = 8;
    public int Score = 0;
    public int Bananas = 0;
    public int CoefBanana = 1;
    public bool isRunning = true;

    public int CountMaxBananas = 0;

    private const string filename = "result.json";
    private const int START_SPEED = 8;

    private const string KEY_SAVE = "mainData";
    public bool onRoad = false;

    public GameData gameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameData = SaveManager.Load<GameData>(KEY_SAVE);
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
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
            Save();
        }
        catch { }

        if (menuLose != null)
        {
            StartCoroutine(ShowMenuLoseSmooth()); // ��������� ������� �������� ���������
        }

        isRunning = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindMenuLose();
    }

    private void FindMenuLose()
    {
        if (menuLose == null)
        {
            GameObject parentObject = GameObject.Find("CanvasPause");
            if (parentObject != null)
            {
                menuLose = parentObject.transform.Find("MenuLose")?.gameObject;
            }
        }

        if (menuLose != null)
        {
            menuLoseCanvasGroup = menuLose.GetComponent<CanvasGroup>();
            if (menuLoseCanvasGroup == null)
            {
                menuLoseCanvasGroup = menuLose.AddComponent<CanvasGroup>();
            }

            menuLoseCanvasGroup.alpha = 0f; // ������ ���� ����������
            menuLose.SetActive(false);
        }
    }

    private IEnumerator ShowMenuLoseSmooth()
    {
        menuLose.SetActive(true);
        menuLoseCanvasGroup.interactable = false; // ��������� �������������� �� ����� ��������
        menuLoseCanvasGroup.blocksRaycasts = false;

        float duration = 0.5f; // ������������ �������� (�������)
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            menuLoseCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menuLoseCanvasGroup.alpha = 1f;
        menuLoseCanvasGroup.interactable = true;
        menuLoseCanvasGroup.blocksRaycasts = true;
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
        // �������� ������ ������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
   
        // ������������� ������� �����
        SceneManager.LoadScene(currentSceneIndex);
        FindMenuLose();
        Bananas = 0;
        Score = 0;
        roadSpeed = START_SPEED;
        CoefBanana = 1;
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

    public readonly List<int> StepsSpeedIncrease = new List<int>() { 15, 30, 50, 100, 170, 300, 500, 700};
    public void IncreaseScore()
    {
        Score += 1;
        if (StepsSpeedIncrease.Contains(Score))
        {
            UpdateSpeed();
        }
    }


}
