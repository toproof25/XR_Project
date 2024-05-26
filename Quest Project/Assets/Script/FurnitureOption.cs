using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FurnitureOption : MonoBehaviour
{
    public GameObject canvas;
    public GrabInteractable grabInteractable;
    public HandGrabInteractable handGrabInteractable;
    public DistanceGrabInteractable distanceGrabInteractable;
    public DistanceHandGrabInteractable distanceHandGrabInteractable;
    public TMP_Text buttonText;

    private bool isGrab = true;

    public float offset = 0.8f;

    void Start()
    {
      
    }

    private void Update()
    {
        //ĵ������ �׻� ���� �ٶ󺸵���
        canvas.transform.LookAt(Camera.main.transform);
        canvas.transform.forward = Camera.main.transform.forward;
    }

    // UI �Ѱ� ����
    public void onCavas() => canvas.SetActive(true);
    public void offCavas() => canvas.SetActive(false);

    // ������Ʈ ���� or ���� ���� �Լ�
    public void LockButtonClick()
    {
        if (isGrab)
        {
            isGrab = false;
            buttonText.text = "Lock";
        }
        else { 
            isGrab = true;
            buttonText.text = "UnLock";
        }

        grabInteractable.enabled = isGrab;
        handGrabInteractable.enabled = isGrab;

        distanceGrabInteractable.enabled = isGrab;
        distanceHandGrabInteractable.enabled = isGrab;
    }

    // ������Ʈ ����
    public void DeleteObject() => Destroy(gameObject);

}
