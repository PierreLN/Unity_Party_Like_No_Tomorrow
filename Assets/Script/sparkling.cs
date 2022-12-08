using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkling : MonoBehaviour
{
    public GameObject sparklo;
    private float timer;

    IEnumerator CSpaarkling()
    {
        while(true)
        {
            timer = Random.Range(1, 5);
            yield return new WaitForSeconds(timer);
            Instantiate(sparklo, transform.position, Quaternion.identity);
        }
    }
    void Start()
    {
        StartCoroutine(CSpaarkling());
    }

    void Update()
    {
        
    }
}
