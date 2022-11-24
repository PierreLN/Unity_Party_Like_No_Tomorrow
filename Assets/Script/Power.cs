using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speedPower = 5.0f;

    private Vector2 direction = new Vector2();

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        direction.y = 0.0f;
        direction.x = 1.0f;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        rig.velocity = direction * speedPower;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Enemie")) ;
        {
            Destroy(this.gameObject);   
        }


    }
}
