using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : SoundsScript
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RockCollider")
        {
            print("Rock");
            PlaySound(sounds[1]);
            collision.collider.enabled = false;
            GameManager.Instance.roadSpeed = 0;
            animator.Play("Hit On Legs", -1, 0f);
            GameManager.Instance.ResetGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        bool flagDestroy = false;
        switch (tag)
        {
            case "Banana":
                GameManager.Instance.Bananas += 1;
                PlaySound(sounds[0], volume: 0.2f);
                flagDestroy = true;
                break;
            case "MushroomDN":
                flagDestroy = true;
                break;
            case "MushroomB":
                flagDestroy = true;
                break;
            case "MushroomMB":
                flagDestroy = true;
                break;
            case "MushroomS":
                flagDestroy = true;
                break;
        }
        if (flagDestroy) Destroy(other.gameObject);

        //if (other.gameObject.tag == "Banana")
        //{
        //    GameManager.Instance.Bananas += 1;
        //    PlaySound(sounds[0], volume: 0.2f);
        //    Destroy(other.gameObject);
        //}
        //else if (other.gameObject.tag == "MushroomDN")
        //{
            
        //}
    }
    private void Update()
    {
        //if (GameManager.Instance.Score > 10)
        //{
        //    GameManager.Instance.roadSpeed = 10;
        //}
        //else if (GameManager.Instance.Score < 10 && GameManager.Instance.roadSpeed == 10)
        //{
        //    GameManager.Instance.roadSpeed = 8;
        //}
    }
}
