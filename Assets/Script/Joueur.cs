using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public float vitesse = 5.0f;
    public float jumpPower = 20.0f;
    private int numberOfJump = 0;

    public GameObject menu;


    private Rigidbody2D rig;   

    Vector2 direction = new Vector2();
    private bool isControlable = true;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        menu.SetActive(false);
    }

    private void Update()
    {
        if (isControlable)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            rig.AddForce(new Vector2(direction.x * vitesse, 0.0f), ForceMode2D.Force);

            if (Input.GetKeyDown(KeyCode.Space) && numberOfJump > 0) 
            {
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Debug.Log(numberOfJump);
                numberOfJump--;
            }
            if (Input.GetButtonDown("pause")) 
            {
                Time.timeScale = 0.0f;
                Debug.Log("Pausing");
                menu.SetActive(true);
            }

        }
    }
    private void FixedUpdate() 
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"));
        {
            Debug.Log("collision");
            if (numberOfJump == 0) 
            {
                numberOfJump++;
            }
        }


    }
}
