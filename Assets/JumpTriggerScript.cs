using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggerScript : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Jump");
    }
}
