using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadExitTrig : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;
    private void OnTriggerEnter(Collider other)
    {
        print("������");
        //_roadSpawner.Spawn();
    }
}
