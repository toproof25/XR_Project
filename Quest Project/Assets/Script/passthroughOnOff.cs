using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class passthroughOnOff : MonoBehaviour
{
    public GameObject passthroughLayer;
    public OVRCameraRig ovrCameraRig;
    private bool onOff = true;

    private GameObject RoomModel;


    private void Update()
    {
        if (RoomModel != null)
        {
            return;
        }

        // OVR Scene Room ������Ʈ�� ���� ������Ʈ�� ã���ϴ�.
        try
        {
            RoomModel = GameObject.FindObjectOfType<OVRSceneRoom>().gameObject;
            //passthrough();
        }

        catch (Exception NullReferenceException)
        {
            Debug.Log("���� ���� �������� ����");
        }
    }

    public void passthrough()
    {
        onOff = !onOff;

        if (onOff)
        {
            passthroughLayer.SetActive(true); // �н����� ���̾� Ȱ��ȭ
            ToggleMeshRenderersInChildren(false);    

            // ī�޶� ����� �����ϰ� ����
            //OVRCameraRig ovrCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
            Camera centerCamera = ovrCameraRig.centerEyeAnchor.GetComponent<Camera>();
            centerCamera.clearFlags = CameraClearFlags.SolidColor;
            centerCamera.backgroundColor = Color.clear;

        }
        else
        {
            passthroughLayer.SetActive(false); 
            ToggleMeshRenderersInChildren(true);
        }
    }

    public void ToggleMeshRenderersInChildren(bool enable)
    {
        // RoomModel �ڽ� ��������
        Transform[] childTransforms = RoomModel.transform.GetComponentsInChildren<Transform>();

        // �ڽ� ��ȸ
        foreach (Transform childTransform in childTransforms)
        {
            // �ڽ� ������Ʈ - Mesh Renderer ã��
            MeshRenderer meshRenderer = childTransform.GetComponent<MeshRenderer>();

            // ������ Ȱ��ȭ/��Ȱ��ȭ
            if (meshRenderer != null)
            {
                meshRenderer.enabled = enable;
            }
        }
    }

}
