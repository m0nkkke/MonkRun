using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //print("бац");
        if (collision.gameObject.tag == "RockCollider")
        {
            print("Rock");
            GameManager.Instance.Score = 0;
        }
        else if (collision.gameObject.tag == "Spider")
        {
            print("Spider");
            GameManager.Instance.Score = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PitTrigger")
        {
            print("Pit");
            GameManager.Instance.Score = 0;
        }
        else if (other.gameObject.name == "RiverTrigger")
        {
            print("River");
            GameManager.Instance.Score = 0;
        }
    }
}
