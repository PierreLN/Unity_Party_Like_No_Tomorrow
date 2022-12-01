using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageIntro : MonoBehaviour
{
    public GameObject msgBox;
    public string nextScene;

    public void starting() 
    {
        SceneManager.LoadScene(nextScene);
    }

    public void quitting() 
    {
        Application.Quit();
    }

    private void Start()
    {
        StartCoroutine(msg_box());
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

}
