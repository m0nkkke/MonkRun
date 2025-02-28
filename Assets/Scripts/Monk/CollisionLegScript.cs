using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLegScript : SoundsScript
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PitTrigger")
        {
            print("Pit");
            PlaySound(sounds[0]);
            GameManager.Instance.ResetGame();
        }
        else if (other.gameObject.name == "RiverTrigger")
        {
            print("River");
            PlaySound(sounds[0]);
            GameManager.Instance.ResetGame();
        }
    }
}
