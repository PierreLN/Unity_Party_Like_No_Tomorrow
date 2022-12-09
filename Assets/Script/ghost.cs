using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    private float speed = 2.0f;
    private string direction = "Gauche";
    private SpriteRenderer render;

    IEnumerator CFlottant()
    {
        while(true){
            transform.position += new Vector3(0.0f, 0.1f, 0.0f);
            yield return new WaitForSeconds(0.3f);
            transform.position -= new Vector3(0.0f, 0.1f, 0.0f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CFlottant());
        render = GetComponent<SpriteRenderer>();
    }

    private static Vector3 Mouvement(string direction, Vector3 position, Vector3 delta)
    {
        if (direction == "Gauche")
        {
            position -= delta;
        }
        else if(direction == "Droite")
        {
            position += delta;
        }

        return position;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == "Droite")
        {
            render.flipX = true;
        }
        else if (direction == "Gauche")
        {
            render.flipX = false;
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("ghostStopper"))
        {
            if(direction == "Gauche")
            {
                direction = "Droite";
            }
            else
            {
                direction = "Gauche";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Power"))
        {
            Destroy(this.gameObject);
        }
    }

}
