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

    void Start()
    {
      
    }

    private void Update()
    {
        //ĵ������ �׻� ���� �ٶ󺸵���
        canvas.transform.localScale = Vector3.one / 1000;
        canvas.transform.LookAt(Camera.main.transform);
        canvas.transform.forward = Camera.main.transform.forward;
    }

    // UI �Ѱ� ����
    public void onCavas() {
        canvas.SetActive(true); 
        canvas.transform.localPosition = new Vector3(0, 1.5f, 0);
    }
    public void offCavas() => canvas.SetActive(false);

    // ������Ʈ ���� or ���� ���� �Լ�
    public void LockButtonClick()
    {
        if (isGrab)
            LockObject();
        else
            UnlockObject();
    }
    private void LockObject()
    {
        isGrab = false;
        grabInteractable.enabled = isGrab;
        handGrabInteractable.enabled = isGrab;

        distanceGrabInteractable.enabled = isGrab;
        distanceHandGrabInteractable.enabled = isGrab;

        buttonText.text = "Lock";
    }
    private void UnlockObject()
    {
        isGrab = true;
        grabInteractable.enabled = isGrab;
        handGrabInteractable.enabled = isGrab; 

        distanceGrabInteractable.enabled = isGrab;
        distanceHandGrabInteractable.enabled = isGrab;

        buttonText.text = "UnLock";
    }

    // ������Ʈ ����
    public void DeleteObject() => Destroy(gameObject);

}
