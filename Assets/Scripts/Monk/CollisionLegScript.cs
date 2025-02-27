using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLegScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PitTrigger")
        {
            print("Pit");
            GameManager.Instance.ResetGame();
        }
        else if (other.gameObject.name == "RiverTrigger")
        {
            print("River");
            GameManager.Instance.ResetGame();
        }
    }
}
