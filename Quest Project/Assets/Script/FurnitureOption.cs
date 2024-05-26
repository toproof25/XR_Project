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
        //캔버스가 항상 나를 바라보도록
        canvas.transform.LookAt(Camera.main.transform);
        canvas.transform.forward = Camera.main.transform.forward;
    }

    // UI 켜고 끄기
    public void onCavas() => canvas.SetActive(true);
    public void offCavas() => canvas.SetActive(false);

    // 오브젝트 고정 or 고정 해제 함수
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

    // 오브젝트 삭제
    public void DeleteObject() => Destroy(gameObject);

}
