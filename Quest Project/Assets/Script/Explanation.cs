using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public GameObject explanationWindow; // 설명창 UI GameObject를 연결할 변수

    private bool isExplanationActive = false;

    void Update()
    {
        if (isExplanationActive) {
            UpdateExplanationPosition();
        }
    }
    private void UpdateExplanationPosition() {
        Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + new Vector3(0, 0.28f, 0); ; // 오른쪽 컨트롤러의 위치
        Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch); // 오른쪽 컨트롤러의 회전
        explanationWindow.transform.position = controllerPosition;
        explanationWindow.transform.rotation = controllerRotation;
    }

    public void ToggleExplanationWindow()
    {
        isExplanationActive = !isExplanationActive;
        if (isExplanationActive) 
        {
            explanationWindow.SetActive(true);
            UpdateExplanationPosition();
        }
        else
        {
            explanationWindow.SetActive(false);
        }
    }
}


