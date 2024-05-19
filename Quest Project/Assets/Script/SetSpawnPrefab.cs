using Oculus.Interaction.DebugTree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetSpawnPrefab : MonoBehaviour
{

    public GameObject copyParent;
    public GameObject copyToggle;
    public GameObject simplePrefabSpawnerObject;

    private SimplePrefabSpawner simplePrefabSpawner;

    public List<GameObject> prefabs = new List<GameObject>(); // 프리팹들
    private List<GameObject> toggles; // 토글즈

    private bool controllerMode = true;

    void Start()
    {
        gameObject.SetActive(false);

        simplePrefabSpawner = simplePrefabSpawnerObject.GetComponent<SimplePrefabSpawner>();
        toggles = new List<GameObject>();

        Debug.Log("--------------------------------------");
        Debug.Log("로드 완료");
        Debug.Log("--------------------------------------");

        SpawnToggle();
    }

    private void Update()
    {
        //Debug.Log(OVRInput.GetActiveController() + " : " + OVRInput.GetActiveController().GetType());
        //Debug.Log(OVRInput.activeControllerType + " : -----------" + OVRInput.activeControllerType == "Touch");

        if (OVRInput.activeControllerType == OVRInput.Controller.Touch)
            controllerMode = true;
        else
            controllerMode = false;

        if (controllerMode)
        {
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) + new Vector3(0, 0.28f, 0);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
        else
        {
            Debug.Log("핸드모드 or 컨트롤러 이외");
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand) + new Vector3(0, 0.28f, 0);
            transform.LookAt(Camera.main.transform);
            transform.forward = Camera.main.transform.forward;

            //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
        }


    }

    private void SpawnToggle()
    {

        for (int i = 0; i < prefabs.Count; i++)
        {
            int index = i;
            GameObject tg = Instantiate(copyToggle, copyParent.transform);

            // TextMeshPro 컴포넌트 가져오기
            TMP_Text textMeshPro = tg.transform.Find("Content").Find("Text").GetComponent<TMP_Text>();

            // 텍스트 설정
            //Debug.Log(prefabs[index].name);
            textMeshPro.text = prefabs[index].name;

            // Toggle 컴포넌트 가져오기
            Toggle toggleComponent = tg.GetComponent<Toggle>();

            // OnValueChanged 이벤트에 SetSpawnPrefab 함수 연결
            toggleComponent.onValueChanged.AddListener(delegate { SpawnIndex(index); });

            tg.SetActive(true);
            toggles.Add(tg);
        }

        gameObject.SetActive(true);
    }


    public void SpawnIndex(int index)
    {
        simplePrefabSpawner.setPrefab(prefabs[index]);
    }

    public void CancelToggle ()
    {
        foreach (GameObject tg in toggles)
        {
            tg.GetComponent<AnimatorOverrideLayerWeigth>().SetOverrideLayerActive(false);
        }
    }


    public void SetActiveUI()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

}
