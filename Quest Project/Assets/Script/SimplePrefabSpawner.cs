using Meta.WitAi;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePrefabSpawner : MonoBehaviour
{
    [Tooltip ("������ ��ġ�Ǵ� ������Ʈ")]
    public GameObject prefab;

    [Tooltip("���� ��ġ�� ���� ǥ�õǴ� ������ ������Ʈ")]
    public GameObject previewPrefab;
    private GameObject currentPreview;

    private bool spawnMode = false;


    private void Start()
    {
        currentPreview = Instantiate(previewPrefab);
        cancel();
    }


    private float rotationSpeed = 100f;
    private float scaleSpeed = 0.5f;
    private float minScale = 0.3f;
    private float maxScale = 2f;

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
            //Debug.Log($"����ĳ��Ʈ�� '{hit.collider.gameObject.name}' ������Ʈ�� �浹�߽��ϴ�. (�±�: {hit.collider.tag}, ��ġ: {hit.point})");
            currentPreview.transform.position = hit.point;
            //currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            if (OVRInput.Get(OVRInput.Button.Left))
            {
                // ���� ȸ��
                currentPreview.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            else if (OVRInput.Get(OVRInput.Button.Right))
            {
                // ������ ȸ��
                currentPreview.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }

            Vector3 currentScale = currentPreview.transform.localScale;
            if (OVRInput.Get(OVRInput.Button.Up))
            {
                // ������ ����
                currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
            }
            else if (OVRInput.Get(OVRInput.Button.Down))
            {
                // ������ ����
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
            }
        }

        spawnMode = true;
        currentPreview.transform.localScale = Vector3.one;
        currentPreview.transform.rotation = Quaternion.Euler(Vector3.zero);
        currentPreview.SetActive(true);
    }

    public void cancel()
    {
        spawnMode = false;
        prefab = null;
        currentPreview.SetActive(false);
    }
}

