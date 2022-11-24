using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrash : MonoBehaviour
{
    public GameObject trash;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(trash, transform.position, Quaternion.identity);
        }
    }
}
