using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public GameObject explanationWindow; // 설명창 UI GameObject를 연결할 변수

    private bool isExplanationActive = false;

    void Update()
    {
        // A 버튼이 눌렸을 때
        if (OVRInput.GetDown(OVRInput.Button.One)) // 예를 들어 A 버튼이 One에 해당한다면
        {
            // 설명창이 활성화되어 있으면 비활성화, 아니면 활성화
            if (isExplanationActive == true)
            {
                isExplanationActive = false;
            }
            else if (isExplanationActive == false)
            {
                isExplanationActive = true;
            }
            // 설명창의 활성화 여부에 따라 처리
            //explanationWindow.SetActive(isExplanationActive);
        }

        if (isExplanationActive)
        {
            // 설명창을 오른쪽 컨트롤러 위치에 따라 이동
            Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + new Vector3(0, 0.28f, 0); ; // 오른쪽 컨트롤러의 위치
            Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch); // 오른쪽 컨트롤러의 회전
            explanationWindow.transform.position = controllerPosition;
            explanationWindow.transform.rotation = controllerRotation;
        }
    }
}


