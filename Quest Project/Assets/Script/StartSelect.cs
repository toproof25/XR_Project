using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSelect : MonoBehaviour
{
    public Button button1;
    public Button button2;

    void Start()
    {
        button1.onClick.AddListener(() => LoadScene(0));
        button2.onClick.AddListener(() => LoadScene(1));
    }

    void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
