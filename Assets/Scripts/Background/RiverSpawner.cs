using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> rivers;
    [SerializeField] private int riverLen;

    private GameObject river;


    private void Start()
    {
        print(transform.position.z);
        river = Instantiate(rivers[Random.Range(0, rivers.Count)], transform.position, Quaternion.identity);
    }

    public void Spawn()
    {
        print(transform.position.z);
        Vector3 pos = new Vector3(river.transform.position.x + riverLen, river.transform.position.y, river.transform.position.z);
        river = Instantiate(rivers[Random.Range(0, rivers.Count)], pos, Quaternion.identity);
    }
}
