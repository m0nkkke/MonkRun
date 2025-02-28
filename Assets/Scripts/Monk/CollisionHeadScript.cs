using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeadScript : SoundsScript
{
    private void OnCollisionEnter(Collision collision)
    {
        //print("head");
        if (collision.gameObject.tag == "Spider")
        {
            print("Spider");
            PlaySound(sounds[0]);
            GameManager.Instance.ResetGame();
        }
        else if (collision.gameObject.tag == "Fly")
        {
            print("Fly");
            PlaySound(sounds[0]);
            GameManager.Instance.ResetGame();
        }
    }
}
