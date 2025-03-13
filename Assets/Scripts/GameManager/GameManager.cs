using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int MaxScore = 0;
    // ����� �����������
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
    public int nextScoreThreshold = 100;
    public bool invertMovement = false;

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
        isRunning = false;

        if (menuLose != null)
        {
            StartCoroutine(ShowMenuLoseSmooth()); // ��������� ������� �������� ���������
        }
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
        try
        {
            Save();
        }
        catch { }
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
        nextScoreThreshold = 100;
        CostRevive = START_COST_REVIVE;
        invertMovement = false;
        isRunning = true;
        isReviveAvailable = false;
        if (pause != null) pause.SetActive(true);
        if (rollbackCoroutine != null) StopCoroutine(rollbackCoroutine);
        mushroomDNCoroutine = null;
        mushroomSpeedCoroutine = null;
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



    #region �����������


    private bool isReviveAvailable = false; // �������� �� �����������
    private Coroutine reviveCoroutine; // ������ �� �������
    private int lastSpeed = 0;
    private const int START_COST_REVIVE = 1;
    public int CostRevive = START_COST_REVIVE;
    private GameObject pause;
    private GameObject reviveMenu;

    public Coroutine mushroomSpeedCoroutine;
    public Coroutine mushroomDNCoroutine;
    private Coroutine rollbackCoroutine;

    public void OnMonkCollision(ColliderTypes collider)
    {
        if (mushroomSpeedCoroutine != null)
        {
            StopCoroutine(mushroomSpeedCoroutine);
            GameObject monk = GameObject.FindWithTag("BackMonk");
            CollisionScript collisionScript = monk.GetComponent<CollisionScript>();
            collisionScript.resetS();
        }
        if (mushroomDNCoroutine != null)
        {
            StopCoroutine(mushroomDNCoroutine);
            GameObject monk = GameObject.FindWithTag("BackMonk");
            CollisionScript collisionScript = monk.GetComponent<CollisionScript>();
            collisionScript.resetDN();
        }
        lastSpeed = roadSpeed;
        pause = GameObject.Find("PauseButton");
        pause.SetActive(false);
        rollbackCoroutine = StartCoroutine(Rollback(collider));

        if (gameData.AllBananas < CostRevive)
        {
            ResetGame();
        }
        else if (!isReviveAvailable)
        {
            isReviveAvailable = true;
            isRunning = false;
            //roadSpeed = 0;
            reviveCoroutine = StartCoroutine(ReviveTimer()); // ��������� �������
        }
    }

    // ������� ��� ������� �����������
    private IEnumerator ReviveTimer()
    {
        reviveMenu.SetActive(true);
        float reviveTimer = 5f; // 5 ������ �� �����������

        while (reviveTimer > 0)
        {
            reviveTimer -= Time.deltaTime;
            yield return null; // ��� ���� ����
        }
        reviveMenu.SetActive(false);
        // ���� ����� �����, ���������� ����
        if (isReviveAvailable)
        {
            ResetGame();
        }
    }

    // ����������� �� �����
    [ContextMenu("�����������")]
    public void Revive()
    {
        if (isReviveAvailable && gameData.AllBananas >= CostRevive)
        {
            reviveMenu.SetActive(false);
            gameData.AllBananas -= CostRevive; // �������� 1 �����
            CostRevive *= 2;
            roadSpeed = lastSpeed;
            isRunning = true;
            GameObject Monk = GameObject.Find("monkWithColider");
            MonkeyController monkeyController = Monk.GetComponent<MonkeyController>();
            monkeyController.PlayAnimation("Running");
            ReplaceFirstFiveRoads(); // �������� ������ 5 �����
            isReviveAvailable = false; // ��������� ����������� �����������
            pause.SetActive(true);
            mushroomDNCoroutine = null;
            mushroomSpeedCoroutine = null;

            // ������������� �������, ���� �� ��� ��������
            if (reviveCoroutine != null)
            {
                StopCoroutine(reviveCoroutine);
                StopCoroutine(rollbackCoroutine);
            }
        }
        else
        {
            Debug.Log("������������ ������� ��� ����� �����");
        }
    }

    // ������ ������ 5 ����� �� EmptyRoad
    private void ReplaceFirstFiveRoads()
    {
        GameObject spawnerObject = GameObject.Find("Spawner");
        RoadSpawner roadSpawner = spawnerObject.GetComponent<RoadSpawner>();
        roadSpawner.ReplaceRoadsWithEmptySegments();

        Debug.Log("������ 5 ����� �������� �� EmptyRoad");
    }

    #endregion

    private IEnumerator Rollback(ColliderTypes collider)
    {
        float rollbackTime = 0.5f; // ����� ������ ����� � ��������
        float elapsedTime = 0f;
        int initialSpeed = roadSpeed;
        int targetSpeed = 0;

        switch (collider)
        {
            case ColliderTypes.Water:
                targetSpeed = -4;
                break;
            case ColliderTypes.Animal:
                targetSpeed = -4;
                break;
            case ColliderTypes.Rock:
                targetSpeed = -4;
                rollbackTime = 0.6f;
                break;
        }

        // ���� ����, ������ ����� ����� �������
        if (collider == ColliderTypes.Water)
        {
            roadSpeed = 0;
            yield return new WaitForSeconds(0.8f);
        }

        // ������� ����� �����
        while (elapsedTime < rollbackTime)
        {
            elapsedTime += Time.deltaTime;
            roadSpeed = Mathf.RoundToInt(Mathf.Lerp(0, targetSpeed, elapsedTime / rollbackTime));
            yield return null;
        }

        // ����������� ����������� � 0
        elapsedTime = 0;
        initialSpeed = roadSpeed;
        while (elapsedTime < rollbackTime)
        {
            elapsedTime += Time.deltaTime;
            roadSpeed = Mathf.RoundToInt(Mathf.Lerp(initialSpeed, 0, elapsedTime / rollbackTime));
            yield return null;
        }

        roadSpeed = 0;
    }


}

public enum ColliderTypes
{
    Animal,
    Rock, 
    Water,
}
