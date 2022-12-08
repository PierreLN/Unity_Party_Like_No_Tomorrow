using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chapeau : MonoBehaviour
{
    private Rigidbody2D rig;
    private float impulsion = 15.0f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(Vector2.up*impulsion, ForceMode2D.Impulse);
        rig.AddForce(Vector2.right * Random.Range(-2.0f, 2.0f), ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }
}
