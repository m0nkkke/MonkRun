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

    private GameObject Monk;
    private void Start()
    {
        Monk = GameObject.Find("monkWithColider");
    }

    private void Update()
    {
        float diff = transform.position.x - Monk.transform.position.x;
        if (flag && diff < 22)
        {
            rend.enabled = true;
            animator.SetTrigger("flag");
            flag = false;
        }
    }
}
