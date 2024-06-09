using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSelect : MonoBehaviour
{
    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }
}
