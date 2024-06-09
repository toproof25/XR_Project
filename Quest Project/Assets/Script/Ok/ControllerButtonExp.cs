using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtonExp : MonoBehaviour
{
    private ControllerButtonsMapper controllerMapper;


    public GameObject LeftCanvas;

    public GameObject voice;
    public GameObject SpawnVoiceBird;
    public GameObject leftJoystick;
    public GameObject menu;



    public GameObject RightCanvas;

    public GameObject cancle;
    public GameObject select;
    public GameObject grab;
    public GameObject ray;

    private bool isAllUI = true;

    private void Awake()
    {
        controllerMapper = GetComponent<ControllerButtonsMapper>();
    }


    public void SetLeftCanvasUI(bool active)
    {
        //LeftCanvas.SetActive(active);
        SetVoiceUI(active);
        SetLeftJoystickUI(active);
        SetMenuUI(active);
        SetVoiceBirdUI(active);
    }


    public void SetRightCanvasUI(bool active) 
    {
        //RightCanvas.SetActive(active);
        SetCancleUI(active);
        SetSelectUI(active);
        SetGrabUI(active);
        SetRaybUI(active);
    }

    public void SetVoiceUI(bool active) => voice.SetActive(active);
    public void SetVoiceBirdUI(bool active) => SpawnVoiceBird.SetActive(active);
    public void SetLeftJoystickUI(bool active) => leftJoystick.SetActive(active);
    public void SetMenuUI(bool active) => menu.SetActive(active);
    public void SetCancleUI(bool active) => cancle.SetActive(active);
    public void SetSelectUI(bool active) => select.SetActive(active);
    public void SetGrabUI(bool active) => grab.SetActive(active);
    public void SetRaybUI(bool active) => ray.SetActive(active);

    public void OnOffUI()
    {
        if (isAllUI)
            isAllUI = false;
        else
            isAllUI = true;

        LeftCanvas.SetActive(isAllUI);
        RightCanvas.SetActive(isAllUI);
    }

    public void SetAllUI(bool active)
    {
        SetVoiceUI(active);
        SetLeftJoystickUI(active);
        SetMenuUI(active);
        SetCancleUI(active);
        SetSelectUI(active);
        SetGrabUI(active);
        SetRaybUI(active);
        SetVoiceBirdUI(active);
    }

    // Voice 설명창 띄우기
    public void VoiceExp()
    {
        if (controllerMapper.controllerMode)
        {
            SetLeftCanvasUI(false);
            SetVoiceUI(true);
            SetVoiceBirdUI(true);
        }
    }

    // 그랩 설명창 띄우기
    public void GrabExp()
    {
        if (controllerMapper.controllerMode)
        {
            SetRightCanvasUI(false);
            SetGrabUI(true);
        }
    }

    // 레이 설명창 띄우기
    public void RayExp()
    {
        if (controllerMapper.controllerMode)
        {
            SetRightCanvasUI(false);
            SetRaybUI(true);
        }
    }

    // 배치 모드 설명창 띄우기
    public void SpawnExp()
    {
        if (controllerMapper.controllerMode)
        {
            SetLeftCanvasUI(false);
            SetRightCanvasUI(false);

            SetCancleUI(true);
            SetSelectUI(true);
            SetLeftJoystickUI(true);
        }
    }



}
