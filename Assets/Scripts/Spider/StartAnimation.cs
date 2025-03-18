using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool flag = true;

    [SerializeField]
    private Renderer rend;

    private void Update()
    {
        if (flag && transform.position.x < 22)
        {
            rend.enabled = true;
            animator.SetTrigger("flag");
            flag = false;
        }
    }
}
