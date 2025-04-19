using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    private void Update()
    {
        DestroyRoad();
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    Move();
    //}

    //private void Move()
    //{
    //    transform.Translate(-transform.right * GameManager.Instance.roadSpeed * Time.fixedDeltaTime);

    //}
    private void DestroyRoad()
    {
        //GameObject spawnerObject = GameObject.Find("Spawner");
        GameObject spawnerObject = GameObject.Find("monkWithColider");
        RoadSpawner roadSpawner = spawnerObject.GetComponent<RoadSpawner>();
        if (spawnerObject.transform.position.x - transform.position.x > 21)
        {
            roadSpawner.AllRoads.RemoveAt(0);
            Destroy(gameObject);
        }
    }
}
