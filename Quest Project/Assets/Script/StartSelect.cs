using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSelect : MonoBehaviour
{
    public void LoadSceneXR()
    {
        SceneManager.LoadScene("XR Project final");
    }

    public void LoadSceneVR()
    {
        SceneManager.LoadScene(1);
    }
}
