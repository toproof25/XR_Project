using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeSceneBtn()
    {
        SceneManager.LoadScene("XR Project"); //이동할 씬이름
    }
}
