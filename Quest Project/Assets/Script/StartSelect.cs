using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSelect : MonoBehaviour
{
    public void LoadSceneXR()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSceneVR()
    {
        SceneManager.LoadScene(2);
    }
}
