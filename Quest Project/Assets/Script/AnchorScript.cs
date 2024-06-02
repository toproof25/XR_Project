using System;
using System.Collections;
using System.Collections.Generic;
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

    private bool isAnchored = true;

    private void Update()
    {
        canvas.transform.LookAt(Camera.main.transform);
        canvas.gameObject.transform.forward = Camera.main.transform.forward;
    }



    public void CreateSpatialAnchor() {

        isAnchored = !isAnchored;

        if (isAnchored == false) {
            anchorPrefab.enabled = true;
            //OVRSpatialAnchor workingAnchor = Instantiate(anchorPrefab, gameObject.transform);
            //canvas = workingAnchor.gameObject.GetComponentInChildren<Canvas>();
            //uuidText = canvas.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            //savedStatusText = canvas.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            StartCoroutine(AnchorCreated(anchorPrefab));
        }
        else
        {
            UnsaveLastCreatedAnchor();
            anchorPrefab.enabled = false;
        }


    }


    private void Start()
    {
        //StartCoroutine(AnchorCreated(anchorPrefab));
    }

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
