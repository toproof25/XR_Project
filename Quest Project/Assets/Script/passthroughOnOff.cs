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

    public LoadSpatialAnchor m_loadSpatialAnchor;

    private void Update()
    {
        if (RoomModel != null)
        {
            return;
        }

        // OVR Scene Room 컴포넌트를 가진 오브젝트를 찾습니다.
        try
        {
            RoomModel = GameObject.FindObjectOfType<OVRSceneRoom>().gameObject;
            Debug.Log("룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행룸실행");
            m_loadSpatialAnchor.LoadAnchorsBtUuid();
            //passthrough();
        }

        catch (Exception NullReferenceException)
        {
            Debug.Log("아직 룸이 생성되지 않음");
        }
    }

    public void passthrough()
    {
        onOff = !onOff;

        if (onOff)
        {
            passthroughLayer.SetActive(true); // 패스스루 레이어 활성화
            ToggleMeshRenderersInChildren(false);    

            // 카메라 배경을 투명하게 설정
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
        // RoomModel 자식 가져오기
        Transform[] childTransforms = RoomModel.transform.GetComponentsInChildren<Transform>();

        // 자식 순회
        foreach (Transform childTransform in childTransforms)
        {
            // 자식 오브젝트 - Mesh Renderer 찾기
            MeshRenderer meshRenderer = childTransform.GetComponent<MeshRenderer>();

            // 있으면 활성화/비활성화
            if (meshRenderer != null)
            {
                meshRenderer.enabled = enable;
            }
        }
    }

}
