using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraduationCloath : MonoBehaviour
{
    private Rigidbody2D rig;
    public Vector2 force = new Vector2(0.0f, 5.0f);
    public Vector3 forceRotation = new Vector3(0.0f, 0.0f, 1.0f);

    IEnumerator CLancer()
    {
        while (true)
        {
            transform.position += forceRotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(CLancer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
