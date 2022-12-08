using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateur_chapeau : MonoBehaviour
{
    private float ah;
    public GameObject Elchapo;
    public GameObject Eltunico;
    private Vector3 position;
    private float second = 0.25f;

    IEnumerator CChapeau()
    {
        while (true)
        {
            yield return new WaitForSeconds(second);
            ah = Random.Range(1, 10);
            position = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, 0.0f);
            if (ah <= 8)
            {
                Instantiate(Elchapo, transform.position + position, Quaternion.identity);
            }
            else
            {
                Instantiate(Eltunico, transform.position + position, Quaternion.identity);
            }
            yield return new WaitForSeconds(second);
            second = Random.Range(0.25f, 0.8f);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CChapeau());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
