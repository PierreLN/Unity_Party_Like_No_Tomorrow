using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packman : MonoBehaviour
{
    private float speed = 15.0f;
    private string direction = "Gauche";
    private SpriteRenderer render;


    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private static Vector3 Mouvement(string direction, Vector3 position, Vector3 delta)
    {
        if (direction == "Gauche")
        {
            position -= delta;
        }
        else if (direction == "Droite")
        {
            position += delta;
        }

        return position;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == "Droite")
        {
            render.flipX = false;
        }
        else if (direction == "Gauche")
        {
            render.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 delta = new Vector3(speed * Time.fixedDeltaTime, 0.0f, 0.0f);
        Vector3 positionCourante = transform.position;
        positionCourante = Mouvement(direction, positionCourante, delta);
        transform.position = positionCourante;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ghostStopper"))
        {
            if (direction == "Gauche")
            {
                direction = "Droite";
            }
            else
            {
                direction = "Gauche";
            }
        }
    }
}
