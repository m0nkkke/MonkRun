using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;
    //[SerializeField] private List<RiverSpawner> _riverSpawners;
    //[SerializeField] private RoadSpawner _mountainSpawner;

    private void OnTriggerEnter(Collider other)
    {
        print("ПРошел");
        _roadSpawner.Spawn();
        //foreach (var riverSpawner in _riverSpawners)
        //{
        //    riverSpawner.Spawn();
        //}
        //_mountainSpawner.Spawn();

    }
}
