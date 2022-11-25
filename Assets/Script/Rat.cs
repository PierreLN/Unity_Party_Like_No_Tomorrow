using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rat : MonoBehaviour
{
    private Vector3 mouvement;
    public float rayonAction = 7.0f;
    private Animator anim;
    public GameObject cible; // cibler le poulet
    public GameObject player;
    public float speed = 2.0f;
    private float distanceVue = 5.0f;
    public LayerMask maskRayon;
    private Rigidbody2D rig;
    private Vector3 startPosition;
    private float distance;


    public enum TRat
    {
        eMange = 1,
        ePatrouille = 2,
        eActif = 4,
        eHome = 8
    }

    public TRat deplacement;


    private bool Bouge()
    {
        return (deplacement & (TRat.ePatrouille | TRat.eActif | TRat.eHome)) != 0;
    }

    private bool EnChasse()
    {
        return deplacement != TRat.eMange;
    }

    private bool RetourHome()
    {
        bool validate = false;
        distance = Vector3.Distance(startPosition, transform.position);
        if (distance >= 7f)
        {
            distanceVue = 0.0f;
            validate = true;
        }
        return validate;

    }

    // Start is called before the first frame update
    void Start()
    {
        mouvement.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPosition = transform.position;

    }



    IEnumerator CPatrouille()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            mouvement = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            mouvement.Normalize();
            yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
            distanceVue = 5.0f;
            mouvement = Vector2.zero;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Bouge())
        {
            anim.SetFloat("Horizontal", mouvement.x);
            anim.SetFloat("Speed", mouvement.sqrMagnitude);
            rig.velocity = mouvement.normalized * speed;
        }

        else
        {
            anim.SetFloat("Speed", 0);
            anim.SetFloat("Horizontal", 1);
            rig.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (!RetourHome())
        {
            if (EnChasse())
            {
                Vector3 direction = cible.transform.position - transform.position;
                direction = direction.normalized;

                RaycastHit2D frappe = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, distanceVue, maskRayon);

                TRat ancienDeplacement = deplacement;
                deplacement = TRat.ePatrouille;

                if (frappe.collider != null)
                {
                    Debug.DrawLine(transform.position, frappe.point);
                    if (frappe.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        // R�ussir � indiquer au Lama qu'il doit rentrer avant de pourchasser le poulet
                        deplacement = TRat.eActif;
                        mouvement = direction;
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, direction * distanceVue);
                }

                if (ancienDeplacement != deplacement)
                {
                    if (deplacement == TRat.ePatrouille)
                    {
                        StartCoroutine(CPatrouille());
                    }
                    else if (deplacement == TRat.eActif)
                    {
                        StopCoroutine(CPatrouille());
                    }
                }
            }
        }
        else
        {
            deplacement = TRat.eHome;
            mouvement = startPosition - transform.position;
            StopCoroutine(CPatrouille());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            deplacement = TRat.eMange;
            speed = 0f;
            rig.velocity = new Vector2(0f, 0f);
            SceneManager.LoadScene("Level_2");
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Power"))
        {
            Destroy(this.gameObject);
        }

    }
}
