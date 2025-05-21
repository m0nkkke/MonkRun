using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLegScript : SoundsScript
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PitTrigger")
        {
            //print("Pit");
            GameSoundManager.Instance.TriggerHitSound();
            other.enabled = false;
            //GameManager.Instance.roadSpeed = 0;
            if (GameManager.Instance.isRunning)
                animator.Play("Sweep Fall", -1, 0f);
            GameManager.Instance.OnMonkCollision(ColliderTypes.Water);
            //GameManager.Instance.ResetGame();
        }
        else if (other.gameObject.name == "RiverTrigger")
        {
            //print("River");
            GameSoundManager.Instance.TriggerHitSound();
            other.enabled = false;
            //GameManager.Instance.roadSpeed = 0;
            if (GameManager.Instance.isRunning)
                animator.Play("Sweep Fall", -1, 0f);
            GameManager.Instance.OnMonkCollision(ColliderTypes.Water);
            //GameManager.Instance.ResetGame();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RoadBottom")
        {
            //print("Õ¿ ƒŒ–Œ√≈");
            GameManager.Instance.onRoad = true;
        }
    }
   
}
