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

    public GameObject bigScrollCopyParent;
    public GameObject bigScrollCopyToggle;

    public GameObject etcScrollCopyParent;
    public GameObject etcScrollCopyToggle;

    public GameObject simplePrefabSpawnerObject;

    private SimplePrefabSpawner simplePrefabSpawner;

    public List<GameObject> prefabsBig = new List<GameObject>(); // 프리팹들
    public List<GameObject> prefabsEtc = new List<GameObject>(); // 프리팹들

    public List<Sprite> big_images = new List<Sprite>(); // 프리팹 이미지들
    public List<Sprite> etc_images = new List<Sprite>(); // 프리팹 이미지들


    private List<GameObject> toggles; // 토글즈

    private List<GameObject> hand_prefabs; // 토글즈

    private bool controllerMode = true;

    public Transform camera;


    void Start()
    {
        simplePrefabSpawner = simplePrefabSpawnerObject.GetComponent<SimplePrefabSpawner>();
        toggles = new List<GameObject>();
        hand_prefabs = new List<GameObject>();


        Debug.Log("--------------------------------------");
        Debug.Log("로드 완료");
        Debug.Log("--------------------------------------");

        SpawnToggle(prefabsBig, big_images, bigScrollCopyToggle, bigScrollCopyParent);
        SpawnButton(prefabsEtc, etc_images, etcScrollCopyToggle, etcScrollCopyParent);

        gameObject.SetActive(false);
        bigScrollCopyToggle.SetActive(false);
        etcScrollCopyToggle.SetActive(false);

        //CancelToggle();
    }

    private void Update()
    {
        if (OVRInput.activeControllerType == OVRInput.Controller.Touch)
            controllerMode = true;
        else
            controllerMode = false;

        // 컨트롤러 사용중이면
        if (controllerMode)
        {
            Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

            // 컨트롤러의 위치에 오브젝트의 위치를 더하고 높이를 조절하여 UI의 위치 설정
            transform.position = controllerPosition + new Vector3(0, 0.28f, 0) + camera.position;
            transform.rotation = controllerRotation * camera.rotation;
        }

    }

    private void SpawnToggle(List<GameObject> prefabs, List<Sprite> images,GameObject copyToggle, GameObject copyParent)
    {

        for (int i = 0; i < prefabs.Count; i++)
        {
            int index = i;

            if (prefabs[index] == null)
                continue;

            GameObject tg = Instantiate(copyToggle, copyParent.transform);

            // TextMeshPro 컴포넌트 가져오기
            TMP_Text textMeshPro = tg.transform.Find("Content").Find("Text").GetComponent<TMP_Text>();
            textMeshPro.text = prefabs[index].name;

            // TextMeshPro 컴포넌트 가져오기
            Image tImage = tg.transform.Find("Content").Find("Background").GetComponent<Image>();
            tImage.sprite = images[index];

            // Toggle 컴포넌트 가져오기
            Toggle toggleComponent = tg.GetComponent<Toggle>();

            // OnValueChanged 이벤트에 SetSpawnPrefab 함수 연결
            toggleComponent.onValueChanged.AddListener(delegate { SpawnIndex(prefabs[index]); });

            tg.SetActive(true);
            toggles.Add(tg);
        }

        gameObject.SetActive(true);
    }

    private void SpawnButton(List<GameObject> prefabs, List<Sprite> images, GameObject copyToggle, GameObject copyParent)
    {

        for (int i = 0; i < prefabs.Count; i++)
        {
            int index = i;

            if (prefabs[index] == null)
                continue;

            GameObject bt = Instantiate(copyToggle, copyParent.transform);

            // TextMeshPro 컴포넌트 가져오기
            TMP_Text textMeshPro = bt.transform.Find("Content").Find("Text").GetComponent<TMP_Text>();
            textMeshPro.text = prefabs[index].name;

            // TextMeshPro 컴포넌트 가져오기
            Image btImage = bt.transform.Find("Content").Find("Background").GetComponent<Image>();
            btImage.sprite = images[index];

            // Toggle 컴포넌트 가져오기
            Button toggleComponent = bt.GetComponent<Button>();

            // OnValueChanged 이벤트에 SetSpawnPrefab 함수 연결
            toggleComponent.onClick.AddListener(delegate { SpawnPrefab(prefabs[index]); });

            bt.SetActive(true);
        }

        gameObject.SetActive(true);
    }


    public void clearHandPrefabs()
    {
        foreach(GameObject pf in hand_prefabs) {
            Debug.Log($"{pf.name} : 사ㄱ제");
            Destroy(pf);
        }

        hand_prefabs.Clear();
    }


    public void SpawnPrefab(GameObject prefab)
    {
        GameObject instantiatedPrefab = Instantiate(prefab);
        instantiatedPrefab.name = prefab.name;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraPosition = Camera.main.transform.position;
        instantiatedPrefab.transform.position = cameraPosition + cameraForward * 0.2f;

        hand_prefabs.Add(instantiatedPrefab);
        //prefab.transform.LookAt(Camera.main.transform);
    }

    public void SpawnIndex(GameObject furniture)
    {
        if (gameObject.activeInHierarchy)
            simplePrefabSpawner.setPrefab(furniture);
    }

    public void CancelToggle()
    {
        foreach (GameObject tg in toggles)
        {
            tg.GetComponent<Toggle>().isOn = false;
            tg.GetComponent<AnimatorOverrideLayerWeigth>().SetOverrideLayerActive(false);
        }
    }


    public void SetActiveUI(bool active)
    {
        gameObject.SetActive(active);

        // 핸드트래킹 모드일 때 UI창 활성화 하면 앞에 스폰하도록
        if (active && !controllerMode)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraPosition = Camera.main.transform.position;
            gameObject.transform.position = cameraPosition + cameraForward * 0.3f;
            gameObject.transform.LookAt(Camera.main.transform);
            gameObject.transform.forward = Camera.main.transform.forward;

            //gameObject.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) + Vector3.forward * 0.2f;
        }
  
    }

}
