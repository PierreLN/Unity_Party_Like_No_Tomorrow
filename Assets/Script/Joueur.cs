using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A faire

// Les jumps animations


public class Joueur : MonoBehaviour
{
    public float vitesse = 40.0f;
    public float jumpPower = 40.0f;
    private int numberOfJump = 1;

    private Animator anim;
    public GameObject menu;
    public GameObject power;
    private Rigidbody2D rig;   
    private SpriteRenderer render;

    Vector2 direction = new Vector2();
    private bool isControlable = true;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        menu.SetActive(false);
    }

    private void Update()
    {
        if (isControlable)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            // Flippage
            if (direction.x < 0) 
            {
                render.flipX = true;
            }
            else 
            {
                render.flipX = false;
            }

            rig.AddForce(new Vector2(direction.x * vitesse, 0.0f), ForceMode2D.Force);
            
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Speed", direction.sqrMagnitude);

            if (Input.GetKeyDown(KeyCode.Space) && numberOfJump > 0) 
            {
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Debug.Log(numberOfJump);
                numberOfJump--;
                Debug.Log("Jumping");
            }
            if (Input.GetButtonDown("pause")) 
            {
                Time.timeScale = 0.0f;
                Debug.Log("Pausing");
                menu.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Instantiate(power, transform.position, Quaternion.identity);       
            }

        }
    }
    private void FixedUpdate() 
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deco")
            || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("collision");
            if (numberOfJump == 0) 
            {
                numberOfJump++;
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
        {
            Debug.Log("Checkpoint");
        }


    }
}
