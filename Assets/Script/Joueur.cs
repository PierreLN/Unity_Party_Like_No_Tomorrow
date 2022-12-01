using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// A faire

// Les jumps animations


public class Joueur : MonoBehaviour
{
    public float vitesse = 10.0f;
    public float jumpPower = 40.0f;
    private int numberOfJump = 1;

    private int nbVie = 3;
    private int nbMunition = 10;

    private UnityAction<object> vie;
    private UnityAction<object> changeMunition;

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


    private void Awake()
    {
        vie = new UnityAction<object>(enleverVie);
        changeMunition = new UnityAction<object>(enleverMunition);

    }

    private void OnEnable()
    {
        EventManager.StartListening("vie", vie);
        EventManager.StartListening("munitionUtiliser", changeMunition);
    }

    private void OnDisable()
    {
        EventManager.StopListening("vie", vie);
        EventManager.StopListening("munitionUtiliser", changeMunition);
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

    void enleverVie(object data)
    {
        nbVie = (int)data;
    }

    void enleverMunition(object data)
    {
        nbMunition = (int)data;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deco")
            || collision.gameObject.layer == LayerMask.NameToLayer("Wall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Blocks"))
        {
            Debug.Log("collision");
            if (numberOfJump == 0) 
            {
                numberOfJump++;
            }
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("EndOfWorld"))
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
