using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    public const string NumUuidsPlayerPref = "numUuids";

    public GameObject canvas;
    public TextMeshProUGUI uuidText;
    public TextMeshProUGUI savedStatusText;
    public TMP_Text buttonText;
    public OVRSpatialAnchor spatialAnchor;


    public bool isAnchored = false;
    public string uuid_string;

    private void Awake()
    {
        Debug.Log(gameObject.name + " -----------------------------------------------------------------------------");
        spatialAnchor = GetComponent<OVRSpatialAnchor>();
    }

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
            //spatialAnchor.enabled = true;
            spatialAnchor = gameObject.AddComponent<OVRSpatialAnchor>();

            buttonText.text = "Lock";

            StartCoroutine(AnchorCreated(spatialAnchor));
        }
        // 앵커가 있으면 제거
        else
        {
            OnEraseButtonPressed();

            buttonText.text = "Unlock";
            uuidText.text = "no UUID";
            savedStatusText.text = "No Anchor";

            //spatialAnchor.enabled = false;
            Destroy(spatialAnchor);
        }
    }



    // 앵커 삭제
    async void OnEraseButtonPressed()
    {
        var result = await spatialAnchor.EraseAnchorAsync();
        if (result.Success)
        {
            Debug.Log($"Successfully erased anchor.");
        }
        else
        {
            Debug.LogError($"Failed to erase anchor {spatialAnchor.Uuid} with result {result.Status}");
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
        spatialAnchor = workingAnchor;

        uuidText.text = "UUID: " + anchorGuid.ToString();
        savedStatusText.text = "Not Saved";
    }


    // 앵커 세이브 함수
    public void SaveAnchor()
    {
        spatialAnchor.Save((savedAnchor, success) => {
            if (success)
            {
                savedStatusText.text = "Saved";
            }
        });

        SaveUuidToPlayerPrefs(spatialAnchor.Uuid);
    }

    void SaveUuidToPlayerPrefs(Guid uuid)
    {
        // 해당 키로 설정된 테이블이 없으면 새로 설정
        if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
        }

        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
        uuid_string = uuid.ToString();
        PlayerPrefs.SetString("uuid" + playerNumUuids, uuid_string);
        PlayerPrefs.SetString(uuid_string, gameObject.name);
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
        PlayerPrefs.Save();
    }


    // 앵커 세이브 해제 함수
    public void UnsaveAnchor()
    {
        spatialAnchor.Erase((erasedAnchor, success) =>
        {
            if (success)
            {
                DeleteUuidByValue(uuid_string);
                savedStatusText.text = "Not Saved";
            }
        });


        
    }

    void DeleteUuidByValue(string uuidValue)
    {
        if (PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);

            for (int i = 0; i < playerNumUuids; i++)
            {
                string key = "uuid" + i;
                if (PlayerPrefs.GetString(key) == uuidValue)
                {
                    PlayerPrefs.DeleteKey(key);
                    PlayerPrefs.DeleteKey(uuidValue);

                    Debug.Log($"Deleted key: {key}");
                    PlayerPrefs.Save();
                    break;
                }
            }
        }
    }



}
