using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerButtonsMapper : MonoBehaviour
{
    public SetSpawnPrefab panelWithManipulators;
    public SimplePrefabSpawner simplePrefabSpawner;
    public passthroughOnOff passthroughOnOff;
    public GameObject PalmMenu;
    
    public GameObject VoicePaner;
    public GameObject mike_on;
    public GameObject mike_off;

    public AppVoiceExperience voiceExperience;



    private bool controllerMode = true;

    

    
    public bool isMike { set; get; }

    private void Start()
    {
        isMike = false;
    }

    private void Update()
    {
        if (OVRInput.activeControllerType == OVRInput.Controller.Touch)
        {
            controllerMode = true;
            PalmMenu.SetActive(false);
        }
        else
        {
            controllerMode = false;
            PalmMenu.SetActive(true);
        }


        if (controllerMode)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                // A버튼 (선택)
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                // B버튼
                if (!panelWithManipulators.gameObject.activeInHierarchy)
                    panelWithManipulators.CancelToggle();
                simplePrefabSpawner.cancel();
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                PassthroughOnOff();
            }
            else if (OVRInput.GetUp(OVRInput.Button.Four))
            {
                Voice();
            }
            else if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ActiveUI();
            }
        }
    }

    public void Voice()
    {
        if (!isMike)
        {
            VoicePaner.transform.position = Camera.main.transform.position + Vector3.forward * 0.5f;
            VoicePaner.transform.LookAt(Camera.main.transform);
            VoicePaner.transform.forward = Camera.main.transform.forward;

            mike_on.SetActive(true);
            mike_off.SetActive(false);

            isMike = true;
            voiceExperience.Activate();
            Debug.Log("마이크 활성화");
        }
        else if (isMike)
        {
            mike_on.SetActive(false);
            mike_off.SetActive(true);

            isMike = false;
            voiceExperience.Deactivate();
            Debug.Log("음소소소소소소솟거");
        }
    }

    public void ActiveUI()
    {
        // = 메뉴
        bool active = !panelWithManipulators.gameObject.activeInHierarchy;
        panelWithManipulators.SetActiveUI(active);
    }

    public void PassthroughOnOff()
    {
        // X버튼
        bool active = !passthroughOnOff.gameObject.activeInHierarchy;
        passthroughOnOff.passthrough(active);
    }
}
