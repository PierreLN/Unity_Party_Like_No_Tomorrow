using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_inGame_ui : MonoBehaviour
{

    public int vie = 3;
    public int munition = 10;

    public TMPro.TextMeshProUGUI affichageVie;
    public TMPro.TextMeshProUGUI affichageMunition;
    private UnityAction<object> changeMunition;
    private UnityAction<object> enleverVie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        changeMunition = new UnityAction<object>(calculerMunition);
        enleverVie = new UnityAction<object>(calculerVie);
    }

    private void OnEnable()
    {
        EventManager.StartListening("vie", enleverVie);
        EventManager.StartListening("munitionUtiliser", changeMunition);
    }

    private void OnDisable()
    {
        EventManager.StopListening("munitionUtiliser", changeMunition);
        EventManager.StopListening("vie", enleverVie);
    }

    public void calculerMunition(object data)
    {
        munition = (int)data;
        affichageMunition.text = munition.ToString();
    }
    
    public void calculerVie(object data)
    {
        vie = (int)data;
        affichageVie.text = vie.ToString();
    }
}
