using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
	public SpriteRenderer fondu;
	public float fadeSpeed = 0.001f;
    public Color couleur;


    private void Start()
	{
		fondu = GetComponent<SpriteRenderer>();
        couleur = new Color(0.0f, 0.0f, 0.0f, 1.0f);
	}


    private void Update()
    {
        couleur.a -= fadeSpeed;
        fondu.color = couleur;
    }
}
