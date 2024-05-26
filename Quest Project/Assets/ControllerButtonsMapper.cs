using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtonsMapper : MonoBehaviour
{
    public SetSpawnPrefab panelWithManipulators;
    public SimplePrefabSpawner simplePrefabSpawner;
    public passthroughOnOff passthroughOnOff;


    private bool controllerMode = true;

    void Start()
    {
        
    }

    private void Update()
    {
        if (OVRInput.activeControllerType == OVRInput.Controller.Touch)
            controllerMode = true;
        else
            controllerMode = false;


        if (controllerMode)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                // A버튼
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                // B버튼
                panelWithManipulators.CancelToggle();
                simplePrefabSpawner.cancel();
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                // X버튼
                passthroughOnOff.passthrough();
            }
            else if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                // Y버튼
            }
            else if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                // = 메뉴
                panelWithManipulators.SetActiveUI();
            }
        }

        


    }
}
