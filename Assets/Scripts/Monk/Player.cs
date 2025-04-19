using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Trigger")
        {
            //print("ПРошел");
            _roadSpawner.Spawn();
            GameManager.Instance.IncreaseScore();
        }
    }
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.right * GameManager.Instance.roadSpeed * Time.fixedDeltaTime);

    }
}
