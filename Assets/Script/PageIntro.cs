using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageIntro : MonoBehaviour
{
    public GameObject msgBox;
    private string nextScene = "Level_1";

    public GameObject fondu_intro;
    private SpriteRenderer fondu_render;
    private float fadeSpeed = 0.0f;
    private Color couleur;


    public void starting() 
    {
        StartCoroutine(change_scene());
    }

    public void quitting() 
    {
        Application.Quit();
    }

    private void Start()
    {
        fondu_render = fondu_intro.GetComponent<SpriteRenderer>();
        couleur = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        StartCoroutine(msg_box());
    }

    private void Update()
    {
        couleur.a += fadeSpeed;
        fondu_render.color = couleur;
    }

    IEnumerator msg_box()
    {
        while (true)
        {
            msgBox.SetActive(false);
            yield return new WaitForSeconds(2.1f);
            msgBox.SetActive(true);
            yield return new WaitForSeconds(5.1f);
            msgBox.SetActive(false);
        }
    }

    IEnumerator change_scene()
    {
        while (true)
        {
            fadeSpeed = 0.005f;
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(nextScene);
        }
    }

}
