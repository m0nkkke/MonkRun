using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeadScript : SoundsScript
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //print("head");
        if (collision.gameObject.tag == "Spider")
        {
            print("Spider");
            PlaySound(sounds[0]);
            collision.collider.enabled = false;
            GameManager.Instance.roadSpeed = 0;
            animator.Play("UmirOtKringa", -1, 0f);
            GameManager.Instance.ResetGame();
        }
        else if (collision.gameObject.tag == "Fly")
        {
            print("Fly");
            PlaySound(sounds[0]);
            collision.collider.enabled = false;
            GameManager.Instance.roadSpeed = 0;
            animator.Play("UmirOtKringa", -1, 0f);
            GameManager.Instance.ResetGame();
        }
    }
}
