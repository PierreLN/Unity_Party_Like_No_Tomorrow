using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageIntro : MonoBehaviour
{
    public string nextScene;

    public void starting() 
    {
        SceneManager.LoadScene(nextScene);
    }

    public void quitting() 
    {
        Debug.Log("quitting...kind of");
    }


}
