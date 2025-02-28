using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : SoundsScript
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RockCollider")
        {
            print("Rock");
            PlaySound(sounds[1]);
            GameManager.Instance.ResetGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Banana")
        {
            GameManager.Instance.Bananas += 1;
            PlaySound(sounds[0], volume: 0.2f);
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (GameManager.Instance.Score > 10)
        {
            GameManager.Instance.roadSpeed = 10;
        }
        else if (GameManager.Instance.Score < 10 && GameManager.Instance.roadSpeed == 10)
        {
            GameManager.Instance.roadSpeed = 8;
        }
    }
}
