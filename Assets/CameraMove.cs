using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(transform.right * GameManager.Instance.roadSpeed * Time.fixedDeltaTime);

    }
}
