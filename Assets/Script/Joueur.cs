using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// A faire

// Les jumps animations


public class Joueur : MonoBehaviour
{
    public float vitesse = 10.0f;
    public float jumpPower = 40.0f;
    private int numberOfJump = 1;

    private int nbVie = 3;
    private int nbMunition = 10;

    private Animator anim;
    public GameObject menu;
    public GameObject power;
    private Rigidbody2D rig;   
    private SpriteRenderer render;
    private Vector3 respawn;
    private bool isJumping = false;
    private bool hit = false;

    private UnityAction<object> victoire;
    //private Vector3 oldPosition = new Vector3(0, 0, 0);

    Vector2 direction = new Vector2();
    private bool isControlable = true;

    IEnumerator CRespawn()
    {
        isControlable = false;
        yield return new WaitForSeconds(1);
        transform.position = respawn;
        isControlable = true;
        hit = false;
        EventManager.TriggerEvent("PlayerHit", false);
    }

    IEnumerator CReload()
    {
        Scene scene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator CVictoire()
    {
        rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.0f);
        rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level_2");
    }

    public void Awake()
    {
        victoire = new UnityAction<object>(ScriptVictoire);
    }

    private void OnEnable()
    {
        EventManager.StartListening("victoire", victoire);
    }

    private void OnDisable()
    {
        EventManager.StopListening("victoire", victoire);
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

            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Speed", direction.sqrMagnitude);

            if (Input.GetButtonDown("pause"))
            {
                Time.timeScale = 0.0f;
                menu.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && numberOfJump > 0)
            {
                //Debug.Log(numberOfJump);
                numberOfJump--;
                isJumping = true;
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) && nbMunition > 0)
            {
                Instantiate(power, transform.position, Quaternion.identity);
                nbMunition--;
                EventManager.TriggerEvent("munitionUtiliser", nbMunition);
            }
        }
    }
    private void FixedUpdate() 
    {

        if (!hit)
        {
            rig.AddForce(new Vector2(direction.x * vitesse, 0.0f), ForceMode2D.Force);
        }

        
        if (isJumping)
        {
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    private void ScriptVictoire(object data)
    {
        if ((bool)data)
        {
            isControlable = false;
            StartCoroutine(CVictoire());
        }
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

        else if (collision.gameObject.layer == LayerMask.NameToLayer("EndOfWorld") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (nbVie > 0)
            {
                if (!hit) nbVie--;
                EventManager.TriggerEvent("vie", nbVie);
                EventManager.TriggerEvent("PlayerHit", true);
                hit = true; 
                StartCoroutine(CRespawn());
            }
            else
            {
                StartCoroutine(CReload());
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
        {
            respawn = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
        }
    }
}
