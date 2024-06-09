using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Voice;
using UnityEngine;

public class VoiceHandle : MonoBehaviour
{
    private ControllerButtonExp controllerButtonExp;

    public SetSpawnPrefab panelWithManipulators;
    public UIPageControll uiPageControll;

    public SimplePrefabSpawner simplePrefabSpawner;
    public passthroughOnOff passthroughOnOff;


    public List<string> prefabs_name = new List<string>(); // 프리팹들 이름
    public List<GameObject> prefabs_model = new List<GameObject>(); // 프리팹들 오브젝트


    private void Start()
    {
        controllerButtonExp = FindObjectOfType<ControllerButtonExp>();
    }

    public void OpenTheUI(string[] values)
    {
        if (values.Length > 0) 
        {
            if (values[0] == "menu") 
            {
                Debug.Log("Menu Open");
                panelWithManipulators.SetActiveUI(true);
                uiPageControll.OnMainScreen();
            }
            else if (values[0] == "option")
            {
                Debug.Log("option gogogogo");
                panelWithManipulators.SetActiveUI(true);
                uiPageControll.OnOptionScreen();
            }
        }
    }


    public void PassthroughOnOff(string[] values)
    {
        if (values.Length > 0)
        {
            if (values[0] == "on")
            {
                Debug.Log("Passthrough ononon");
                passthroughOnOff.passthrough(true);
            }
            else if (values[0] == "off")
            {
                Debug.Log("Passthrough off");
                passthroughOnOff.passthrough(false);
            }
        }
    }


    public void CreateObject(string[] values)
    {
        if (values.Length > 0)
        {
            Debug.Log($"Create object : {values[0]}");

            for(int i=0; i < prefabs_name.Count; i++)
            {
                if (values[0] == prefabs_name[i])
                {
                    GameObject instantiatedPrefab = Instantiate(prefabs_model[i],
                        OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + Vector3.forward,
                        Quaternion.FromToRotation(Vector3.zero, Vector3.forward));

                    instantiatedPrefab.name = instantiatedPrefab.name.Replace("(Clone)", "");

                    OVRSpatialAnchor spatialAnchor = instantiatedPrefab.GetComponent<OVRSpatialAnchor>();
                    Destroy(spatialAnchor);
                }
            }
        }
    }


    public void OpenTheVoiceExp(bool active)
    {
        if (active)
            controllerButtonExp.VoiceExp();
        else
            controllerButtonExp.SetAllUI(true);
    }

}