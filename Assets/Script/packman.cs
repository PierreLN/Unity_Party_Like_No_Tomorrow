using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class packman : MonoBehaviour
{
    public float speed = 15.0f;
    private float cacheSpeed = 0.0f;
    public string direction = "Gauche";
    private SpriteRenderer render;
    private UnityAction<object> playerHit;


    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void Awake()
    {
        playerHit = new UnityAction<object>(changeSpeed);
    }

    private void OnEnable()
    {
        EventManager.StartListening("PlayerHit", playerHit);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerHit", playerHit);
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

    void changeSpeed(object data)
    {
        if ((bool)data)
        {
            cacheSpeed = speed;
            speed = 0.0f;
            Debug.Log(speed);
        }
        else
        {
            speed = cacheSpeed;
            Debug.Log(speed);
        }
    }
}
