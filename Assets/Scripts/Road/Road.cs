using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private void Update()
    {
        DestroyRoad();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.right * _speed * Time.fixedDeltaTime);

    }
    private void DestroyRoad()
    {
        if (transform.position.x < -5f)
        {
            Destroy(gameObject);
        }
    }
}
