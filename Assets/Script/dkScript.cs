using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dkScript : MonoBehaviour
{

    private Animator anim;
    
    IEnumerator CTirerBarril()
    {
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("Launch", true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("Launch", false);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Power"))
        {
            EventManager.TriggerEvent("victoire", true);
            anim.SetBool("Defeat", true);
        }
    }
}
