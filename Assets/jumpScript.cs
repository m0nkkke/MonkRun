using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class jumpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        //Input.GetKey(KeyCode.Space);
        //Input.GetKeyDown(KeyCode.B);
        //Input.GetMouseButtonDown(1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //int c = 0;

            for (int i = 0; i < 250; i++)
            {
                transform.Translate(0f, 0.01f, 0f);
            }
            //while (transform.position.y < 1)
            //{
            //    transform.Translate(0f, 0.01f, 0f);
            //}
            //while (transform.position.y > 0.1)
            //{
            //    transform.Translate(0f, -0.01f, 0f);
            //}
            //transform.Translate(0f, 2f, 0f);
            //transform.Translate(0f, -1f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.01f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, 0.01f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.01f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, -0.01f);
        }
        if (Input.GetMouseButtonDown(1)) transform.position = new Vector3(0,0,0);
    }
    private void LateUpdate()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
