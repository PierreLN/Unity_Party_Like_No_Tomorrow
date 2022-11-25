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
    private UnityAction<object> explosion;
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
        explosion = new UnityAction<object>(calculerMunition);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Boom", explosion);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Boom", explosion);
    }

    public void calculerMunition(object data)
    {
        if (munition > 0)
        {
            munition--;
            EventManager.TriggerEvent("munitionUtiliser", munition);
        }

        affichageMunition.text = munition.ToString();
    }
}
