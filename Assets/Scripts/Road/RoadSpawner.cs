using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RoadSpawner : MonoBehaviour
{
    private List<GameObject> roads;
    [SerializeField] private int roadLen;

    [SerializeField] private List<GameObject> EmptyRoads;
    [SerializeField] private List<GameObject> RockRoads;
    [SerializeField] private List<GameObject> BananaRoads;
    [SerializeField] private List<GameObject> FlyRoads;
    [SerializeField] private List<GameObject> SpiderRoads;
    [SerializeField] private List<GameObject> RiverRoads;
    [SerializeField] private List<GameObject> PitRoads;
    [SerializeField] private List<GameObject> MushroomRoads;

    private float coefEmpty = 10f;
    private float coefRock = 20f;
    private float coefBanana = 10f;
    private float coefFly = 20f;
    private float coefSpider = 10f;
    private float coefRiver = 10f;
    private float coefPit = 10f;
    private float coefMushroom = 10f;

    private GameObject road;

    private List<int> posEmptySpawnTrigger;
    private int countEmptySpawns = 0;

    [SerializeField]
    private bool mushrooms = false;

    private void Start() 
    {
        roads = EmptyRoads.Concat(RockRoads)
            .Concat(BananaRoads)
            .Concat(FlyRoads)
            .Concat(SpiderRoads)
            .Concat(RiverRoads)
            .Concat(PitRoads)
            .ToList();
        if (mushrooms) roads = roads.Concat(MushroomRoads).ToList();
        road = Instantiate(roads[Random.Range(0, roads.Count)], transform.position, Quaternion.identity);
        //posEmptySpawnTrigger = GameManager.Instance.StepsSpeedIncrease.Select(x => (x - 10)).ToList();
    }

    public void Spawn()
    {
        //int score = (GameManager.Instance.Score / 10) % 10;
        //int score = (GameManager.Instance.Score / 10) % 2;
        int score = GameManager.Instance.Score;

        //if (score == 1) SpawnEmpty();
        int diff = GameManager.Instance.StepsSpeedIncrease[countEmptySpawns] - score;
        if (diff <= 15 && diff > 13)
        {
            SpawnEmpty();
            if (countEmptySpawns < GameManager.Instance.StepsSpeedIncrease.Count - 1 && GameManager.Instance.StepsSpeedIncrease.Contains(score + 1))
                countEmptySpawns += 1;
        }
        else SpawnRoadByCategory(ChooseSegment());
        //else SpawnRoad(roads);
    }
    private void SpawnEmpty()
    {
        SpawnRoad(EmptyRoads);
    }

    private void SpawnRoadByCategory(CategorySegment cat)
    {
        switch (cat)
        {
            case CategorySegment.Empty:
                SpawnRoad(EmptyRoads);
                break;
            case CategorySegment.Rock:
                SpawnRoad(RockRoads);
                break;
            case CategorySegment.Pit:
                SpawnRoad(PitRoads);
                break;
            case CategorySegment.Spider:
                SpawnRoad(SpiderRoads);
                break;
            case CategorySegment.Banana:
                SpawnRoad(BananaRoads);
                break;
            case CategorySegment.Fly:
                SpawnRoad(FlyRoads);
                break;
            case CategorySegment.River:
                SpawnRoad(RiverRoads);
                break;
            case CategorySegment.Mushroom:
                SpawnRoad(MushroomRoads);
                break;
        }
    }

    private void SpawnRoad(List<GameObject> roadList)
    {
        if (road.transform.position.x < 100)
        {
            Vector3 pos = new Vector3(road.transform.position.x + roadLen, road.transform.position.y, road.transform.position.z);
            road = Instantiate(roadList[Random.Range(0, roadList.Count)], pos, Quaternion.identity);
        }
    }

    // Метод для выбора сегмента
    public CategorySegment ChooseSegment()
    {
        // Создаем список с коэффициентами
        List<float> coefficients = new List<float> { coefEmpty, coefRock, coefBanana, coefFly, coefSpider, coefRiver, coefPit, coefMushroom };
        List<CategorySegment> segmentNames = new List<CategorySegment> 
        { 
            CategorySegment.Empty, 
            CategorySegment.Rock, 
            CategorySegment.Banana, 
            CategorySegment.Fly,
            CategorySegment.Spider,
            CategorySegment.River, 
            CategorySegment.Pit,
            CategorySegment.Mushroom 
        };

        // Генерируем случайное число от 0 до 100
        float randomValue = Random.Range(0f, 100f);

        // Проходим по списку коэффициентов и выбираем сегмент
        float cumulativeProbability = 0f;
        for (int i = 0; i < coefficients.Count; i++)
        {
            cumulativeProbability += coefficients[i];
            if (randomValue <= cumulativeProbability)
            {
                return segmentNames[i];
            }
        }

        // Если что-то пошло не так, возвращаем пустой сегмент
        return CategorySegment.Empty;
    }


}

public enum CategorySegment
{
    Empty,
    Rock,
    Banana,
    Fly,
    Spider,
    River,
    Pit,
    Mushroom,
}
