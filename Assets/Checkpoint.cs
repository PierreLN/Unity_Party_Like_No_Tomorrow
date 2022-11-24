using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    IEnumerator CAnimation()
    {
        anim.SetBool("Activated", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Activated", false);
        anim.SetBool("Active", true);

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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(CAnimation());

            //anim.SetBool("Activated", true);
        }
    }


    void onAnimationEnd()
    {
        anim.SetBool("Active", true);
    }
}
