using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A faire

// Les jumps animations


public class Joueur : MonoBehaviour
{
    public float vitesse = 10.0f;
    public float jumpPower = 40.0f;
    private int numberOfJump = 1;

    private Animator anim;
    public GameObject menu;
    public GameObject power;
    private Rigidbody2D rig;   
    private SpriteRenderer render;
    private Vector3 respawn;
    //private Vector3 oldPosition = new Vector3(0, 0, 0);

    Vector2 direction = new Vector2();
    private bool isControlable = true;

    IEnumerator CRespawn()
    {
        Debug.Log(respawn);
        yield return new WaitForSeconds(1);
        transform.position = respawn;
    }

    private void Start()
    {
        respawn = transform.position;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        menu.SetActive(false);
    }

    private void Update()
    {
        // Éventuellement calculer la vitesse
        /*float speed = Vector3.Distance(oldPosition, transform.position);
        oldPosition = transform.position;*/

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

        if (collision.gameObject.layer == LayerMask.NameToLayer("EndOfWorld"))
        {
            Debug.Log("test");
            StartCoroutine(CRespawn());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
        {
            Debug.Log("Checkpoint");
            respawn = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
        }
    }
}
