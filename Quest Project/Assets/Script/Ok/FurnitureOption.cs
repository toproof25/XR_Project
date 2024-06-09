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
    private ControllerButtonExp controllerButtonExp;

    public GameObject canvas;
    public GrabInteractable grabInteractable;
    public HandGrabInteractable handGrabInteractable;
    public DistanceGrabInteractable distanceGrabInteractable;
    public DistanceHandGrabInteractable distanceHandGrabInteractable;
    public TMP_Text buttonText;


    // 중력 on off
    private Rigidbody rigidbody;
    private bool isGravity = false;
    public TMP_Text gravityText;


    //private Vector3 initialScale; // UI스케일 고정


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        gravityText.color = Color.white;
    }

    void Start()
    {
        controllerButtonExp = FindObjectOfType<ControllerButtonExp>();
    }

    private void LateUpdate()
    {
        //캔버스가 항상 나를 바라보도록 / 크기 고정
        canvas.transform.LookAt(Camera.main.transform);
        canvas.transform.forward = Camera.main.transform.forward;
        //canvas.transform.localScale = initialScale;
    }

    // UI 켜고 끄기
    public void onCavas() => canvas.SetActive(true);
    public void offCavas() => canvas.SetActive(false);


    public void GravityOnOff()
    {
        if (isGravity)
        {
            isGravity = false;
            rigidbody.isKinematic = true;
            gravityText.color = Color.white;
        }
        else
        {
            isGravity = true;
            rigidbody.isKinematic = false;
            gravityText.color = Color.blue;
        }
    }


    public void RayUI(bool active)
    {
        if (active)
            controllerButtonExp.RayExp();
        else
            controllerButtonExp.SetAllUI(true);
    }

    public void GrabUI(bool active)
    {
        if (active)
            controllerButtonExp.GrabExp();
        else
            controllerButtonExp.SetAllUI(true);
    }
}
