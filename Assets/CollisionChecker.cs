using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.CompareTag);
        // ���������, � ��� �����������
        if (collision.gameObject.CompareTag("Stone"))
        {
            Debug.Log("������������ � ������!");
        }
        else if (collision.gameObject.CompareTag("Spider"))
        {
            Debug.Log("������������ � ������!");
        }
    }
}
