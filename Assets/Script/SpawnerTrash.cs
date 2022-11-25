using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrash : MonoBehaviour
{
    public GameObject cone;
    public GameObject stopSign;
    private Vector3 direction;
    void Start()
    {
        direction.y = 5.0f;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cone, transform.position + direction, Quaternion.identity);
            Instantiate(stopSign, transform.position, Quaternion.identity);
        }
    }
}
