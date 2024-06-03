using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    public OVRSpatialAnchor anchorPrefab;

    public const string NumUuidsPlayerPref = "numUuids";

    public GameObject canvas;
    public TextMeshProUGUI uuidText;
    public TextMeshProUGUI savedStatusText;

    private List<OVRSpatialAnchor> anchors = new List<OVRSpatialAnchor>();
    private OVRSpatialAnchor lastCreatedAnchor;

    private bool isAnchored = false;
    private bool isSaved = false;

    private void Update()
    {
        canvas.transform.LookAt(Camera.main.transform);
        canvas.gameObject.transform.forward = Camera.main.transform.forward;
    }


    // 앵커버튼 누르면 생성 or 제거
    public void CreateSpatialAnchor() {

        isAnchored = !isAnchored;

        // 앵커가 없으면 생성
        if (isAnchored == false) {
            //anchorPrefab.enabled = true;
            anchorPrefab = gameObject.AddComponent<OVRSpatialAnchor>();
            StartCoroutine(AnchorCreated(anchorPrefab));
        }
        // 앵커가 있으면 제거
        else
        {
            OnEraseButtonPressed();
            uuidText.text = "no UUID";
            savedStatusText.text = "No Anchor";
            anchors.Clear();
            //anchorPrefab.enabled = false;
            Destroy(anchorPrefab);
        }
    }


    public void SavedOrUnsaved()
    {
        isSaved = !isSaved;

        // 세이브가 안돼있으면 세이브
        if (isSaved == true)
        {
            SaveLastCreatedAnchor();
        }
        // 세이브가 돼있으면 언세이브
        else
        {
            UnsaveLastCreatedAnchor();
        }

    }
    

    // 앵커 삭제
    async void OnEraseButtonPressed()
    {
        var result = await anchorPrefab.EraseAnchorAsync();
        if (result.Success)
        {
            Debug.Log($"Successfully erased anchor.");
        }
        else
        {
            Debug.LogError($"Failed to erase anchor {anchorPrefab.Uuid} with result {result.Status}");
        }
    }


    // 앵커 생성
    private IEnumerator AnchorCreated(OVRSpatialAnchor workingAnchor)
    {
        while (!workingAnchor.Created && !workingAnchor.Localized)
        {
            yield return new WaitForEndOfFrame();
        }

        Guid anchorGuid = workingAnchor.Uuid;
        anchors.Add(workingAnchor);
        lastCreatedAnchor = workingAnchor;

        uuidText.text = "UUID: " + anchorGuid.ToString();
        savedStatusText.text = "Not Saved";
    }


    // 앵커 세이브 함수
    public void SaveLastCreatedAnchor()
    {
        lastCreatedAnchor.Save((lastCreatedAnchor, success) => {
            if (success)
            {
                savedStatusText.text = "Saved";
            }
        });

        SaveUuidToPlayerPrefs(lastCreatedAnchor.Uuid);
    }

    void SaveUuidToPlayerPrefs(Guid uuid)
    {
        // 해당 키로 설정된 테이블이 없으면 새로 설정
        if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
        }

        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
        PlayerPrefs.SetString("uuid" + playerNumUuids, uuid.ToString());
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);

    }

    // 앵커 세이브 해제 함수
    public void UnsaveLastCreatedAnchor()
    {
        lastCreatedAnchor.Erase((lastCreatedAnchor, success) =>
        {
            if (success)
            {
                savedStatusText.text = "Not Saved";
            }
        });

    }


}
