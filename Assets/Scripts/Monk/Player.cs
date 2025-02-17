using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;
    private void OnTriggerEnter(Collider other)
    {
        print("ПРошел");
        _roadSpawner.Spawn();
    }
}
