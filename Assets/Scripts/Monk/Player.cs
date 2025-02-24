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
        if (other.gameObject.name == "Trigger")
        {
            print("ПРошел");
            _roadSpawner.Spawn();
            GameManager.Instance.Score += 1;
        }
    }
}
