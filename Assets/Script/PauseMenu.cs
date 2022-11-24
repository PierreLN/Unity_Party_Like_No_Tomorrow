using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string firstScene;
    public GameObject menu;


    public void continueScene() 
    {
        Debug.Log("continue");
        Time.timeScale = 1.0f;
        menu.SetActive(false);
    }

    public void backToIntro() 
    {
        SceneManager.LoadScene(firstScene);
    }
}
