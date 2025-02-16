using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.CompareTag);
        // Проверяем, с чем столкнулись
        if (collision.gameObject.CompareTag("Stone"))
        {
            Debug.Log("Столкновение с камнем!");
        }
        else if (collision.gameObject.CompareTag("Spider"))
        {
            Debug.Log("Столкновение с пауком!");
        }
    }
}
