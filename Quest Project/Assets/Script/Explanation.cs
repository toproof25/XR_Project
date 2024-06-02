using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public GameObject explanationWindow; // ����â UI GameObject�� ������ ����

    private bool isExplanationActive = false;

    void Update()
    {
        // A ��ư�� ������ ��
        if (OVRInput.GetDown(OVRInput.Button.One)) // ���� ��� A ��ư�� One�� �ش��Ѵٸ�
        {
            // ����â�� Ȱ��ȭ�Ǿ� ������ ��Ȱ��ȭ, �ƴϸ� Ȱ��ȭ
            if (isExplanationActive == true)
            {
                isExplanationActive = false;
            }
            else if (isExplanationActive == false)
            {
                isExplanationActive = true;
            }
            // ����â�� Ȱ��ȭ ���ο� ���� ó��
            //explanationWindow.SetActive(isExplanationActive);
        }

        if (isExplanationActive)
        {
            // ����â�� ������ ��Ʈ�ѷ� ��ġ�� ���� �̵�
            Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + new Vector3(0, 0.28f, 0); ; // ������ ��Ʈ�ѷ��� ��ġ
            Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch); // ������ ��Ʈ�ѷ��� ȸ��
            explanationWindow.transform.position = controllerPosition;
            explanationWindow.transform.rotation = controllerRotation;
        }
    }
}


