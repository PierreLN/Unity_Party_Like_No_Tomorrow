using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dkScript : MonoBehaviour
{

    public GameObject cible;
    public GameObject projectile;
    public GameObject frappe;
    public GameObject lieuInstanciation;
    public LayerMask maskRayon;

    private Animator anim;
    private float distanceVue = 25.0f;
    private bool vue = false;
    private bool coroutineActive = false;

    IEnumerator CTirerBarril()
    {
        Debug.Log("in");
        while (!anim.GetBool("Defeat") && vue)
        {
            Debug.Log("defaite est false");
            anim.SetBool("Launch", true);
            yield return new WaitForSeconds(0.55f);
            anim.SetBool("Launch", false);
            yield return new WaitForSeconds(0.1f);
            GameObject clone = (GameObject)Instantiate(frappe, lieuInstanciation.transform.position, Quaternion.identity);
            Instantiate(projectile, lieuInstanciation.transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
            yield return new WaitForSeconds(7.0f);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 direction = cible.transform.position - transform.position;
        direction = direction.normalized;

        RaycastHit2D frappe = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, distanceVue, maskRayon);

        if (frappe.collider != null)
        {
            Debug.DrawLine(transform.position, frappe.point);
            if (frappe.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                vue = true;
                if (!coroutineActive)
                {
                    StartCoroutine(CTirerBarril());
                    coroutineActive = true;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction * distanceVue);
            coroutineActive = false;
            vue = false;
            //StopCoroutine(CTirerBarril());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Power"))
        {
            EventManager.TriggerEvent("victoire", true);
            anim.SetBool("Defeat", true);
        }
    }
}
