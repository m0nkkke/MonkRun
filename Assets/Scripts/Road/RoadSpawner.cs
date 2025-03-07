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

    private GameObject road;

    private List<int> posEmptySpawnTrigger;
    private int countEmptySpawns = 0;

    private void Start() 
    {
        roads = EmptyRoads.Concat(RockRoads)
            .Concat(BananaRoads)
            .Concat(FlyRoads)
            .Concat(SpiderRoads)
            .Concat(RiverRoads)
            .Concat(PitRoads)
            .ToList();
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
        if (diff <= 10 && diff > 7)
        {
            SpawnEmpty();
            if (countEmptySpawns < GameManager.Instance.StepsSpeedIncrease.Count - 1 && GameManager.Instance.StepsSpeedIncrease.Contains(score + 1))
                countEmptySpawns += 1;
        }
        else SpawnRoad(roads);
    }
    private void SpawnEmpty()
    {
        SpawnRoad(EmptyRoads);
    }

    private void SpawnRoad(List<GameObject> roadList)
    {
        if (road.transform.position.x < 100)
        {
            Vector3 pos = new Vector3(road.transform.position.x + roadLen, road.transform.position.y, road.transform.position.z);
            road = Instantiate(roadList[Random.Range(0, roadList.Count)], pos, Quaternion.identity);
        }
    }


}
