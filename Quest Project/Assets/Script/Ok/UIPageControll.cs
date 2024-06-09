using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPageControll : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject optionScreen;


    void Start()
    {
        OnMainScreen();
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


    public void LobbyScene()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Lobby");
    }
}
