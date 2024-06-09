using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    public Button button1;
    public Button button2;

    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        if (button1 != null)
        {
            button1.onClick.AddListener(() => LoadSceneByIndex(0));
        }
        if (button2 != null)
        {
            button2.onClick.AddListener(() => LoadSceneByIndex(1));
        }
    }

    void LoadSceneByIndex(int index)
    {
        // 인덱스에 해당하는 씬 로드
        SceneManager.LoadScene(index);
    }
}
