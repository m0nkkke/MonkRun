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
    }

    public void Spawn()
    {
        int score = (GameManager.Instance.Score / 10) % 10;
        if (score == 4) SpawnEmpty();
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
