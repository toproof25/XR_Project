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
                // A��ư
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                // B��ư
                panelWithManipulators.CancelToggle();
                simplePrefabSpawner.cancel();
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                // X��ư
                passthroughOnOff.passthrough();
            }
            else if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                // Y��ư
            }
            else if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                // = �޴�
                panelWithManipulators.SetActiveUI();
            }
        }

        


    }
}
