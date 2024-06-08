using Meta.WitAi;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePrefabSpawner : MonoBehaviour
{
    [Tooltip ("실제로 설치되는 오브젝트")]
    public GameObject prefab;

    [Tooltip("벽에 설치될 곳이 표시되는 가상의 오브젝트")]
    public GameObject previewPrefab;
    private GameObject currentPreview;

    private bool spawnMode = false;


    private float rotationSpeed = 100f;
    private float scaleSpeed = 0.5f;
    private float minScale = 0.3f;
    private float maxScale = 2f;


    private void Start()
    {
        currentPreview = Instantiate(previewPrefab);
        cancel();
    }


    private void Update()
    {
        if (!spawnMode)
        {
            return;
        }

        Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log($"레이캐스트가 '{hit.collider.gameObject.name}' 오브젝트와 충돌했습니다. (태그: {hit.collider.tag}, 위치: {hit.point})");
            currentPreview.transform.position = hit.point;
            //currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            if (OVRInput.Get(OVRInput.Button.Left))
            {
                // 왼쪽 회전
                currentPreview.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            else if (OVRInput.Get(OVRInput.Button.Right))
            {
                // 오른쪽 회전
                currentPreview.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }

            Vector3 currentScale = currentPreview.transform.localScale;
            if (OVRInput.Get(OVRInput.Button.Up))
            {
                // 스케일 증가
                currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
            }
            else if (OVRInput.Get(OVRInput.Button.Down))
            {
                // 스케일 감소
                currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            }

            currentScale.x = Mathf.Clamp(currentScale.x, minScale, maxScale);
            currentScale.y = Mathf.Clamp(currentScale.y, minScale, maxScale);
            currentScale.z = Mathf.Clamp(currentScale.z, minScale, maxScale);
            currentPreview.transform.localScale = currentScale;



            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                GameObject instantiatedPrefab = Instantiate(prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                instantiatedPrefab.transform.localScale = currentPreview.transform.localScale;
                instantiatedPrefab.transform.rotation = currentPreview.transform.rotation;

                OVRSpatialAnchor spatialAnchor = instantiatedPrefab.GetComponent<OVRSpatialAnchor>();
                Destroy(spatialAnchor);
                cancel();
            }
        }
    }

   
    public void setPrefab(GameObject pf)
    {
        prefab = pf;

        Mesh mesh = null;
        Transform visualsChild = prefab.transform.Find("Visuals");
        if (visualsChild != null)
        {
            Transform meshChild = visualsChild.Find("Mesh");
            if (meshChild != null)
            {
                mesh = meshChild.GetComponent<MeshFilter>().sharedMesh;
                currentPreview.GetComponent<MeshFilter>().sharedMesh = mesh;

                spawnMode = true;
                currentPreview.transform.localScale = Vector3.one;
                currentPreview.transform.rotation = Quaternion.Euler(Vector3.zero);
                currentPreview.SetActive(true);
            }
        }
        else
        {
            cancel();
            return;
        }


    }

    public void cancel()
    {
        spawnMode = false;
        prefab = null;
        currentPreview.SetActive(false);
    }
}

