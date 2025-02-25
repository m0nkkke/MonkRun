using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeadScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //print("head");
        if (collision.gameObject.tag == "Spider")
        {
            print("Spider");
            GameManager.Instance.Score = 0;
        }
        else if (collision.gameObject.tag == "Fly")
        {
            print("Fly");
            GameManager.Instance.Score = 0;
        }
    }
}
