using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageControll : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject optionScreen;


    void Start()
    {
        OnMainScreen();
    }

    
    void Update()
    {
        
    }


    public void OnOptionScreen()
    {
        mainScreen.SetActive(false);
        optionScreen.SetActive(true);
    }
    public void OnMainScreen()
    {
        optionScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
}
