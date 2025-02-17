using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads;
    [SerializeField] private int roadLen;

    private GameObject road;

    private void Start()
    {
        road = Instantiate(roads[Random.Range(0, roads.Count)], transform.position, Quaternion.identity);
    }

    public void Spawn()
    {
        Vector3 pos = new Vector3(road.transform.position.x + roadLen, road.transform.position.y, road.transform.position.z );
        road = Instantiate(roads[Random.Range(0, roads.Count)], pos, Quaternion.identity);
    }


}
