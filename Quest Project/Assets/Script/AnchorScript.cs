using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    public const string NumUuidsPlayerPref = "numUuids";

    public GameObject canvas;
    public TextMeshProUGUI uuidText;
    public TextMeshProUGUI savedStatusText;
    public OVRSpatialAnchor spatialAnchor;

    public TMP_Text buttonText;
    public TMP_Text saveText;
    public TMP_Text unSaveText;


    public bool isAnchored = true;
    public string uuid_string = null;

    private bool isRemove = false;

    private void Awake()
    {
        Debug.Log(gameObject.name + " -----------------------------------------------------------------------------");
        spatialAnchor = GetComponent<OVRSpatialAnchor>();

        buttonText.color = Color.white;
        saveText.color = Color.white;
        unSaveText.color = Color.white;
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
        if (isAnchored == true) {
            if (spatialAnchor == null)
                spatialAnchor = gameObject.AddComponent<OVRSpatialAnchor>();
            else
                spatialAnchor.enabled = true;

            StartCoroutine(AnchorCreated(spatialAnchor));
        }
        // 앵커가 있으면 제거
        else
        {
            spatialAnchor.enabled = false;
            OnEraseButtonPressed();  
        }
    }



    // 앵커 삭제 (저장 제거)
    async void OnEraseButtonPressed()
    {
        var result = await spatialAnchor.EraseAnchorAsync();
        if (result.Success)
        {
            buttonText.text = "Unlock";
            uuidText.text = "no UUID";
            savedStatusText.text = "No Anchor";

            buttonText.color = Color.white;

            DeleteUuidByValue(uuid_string);
            Destroy(spatialAnchor);
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
        uuid_string = anchorGuid.ToString();
        uuidText.text = "UUID: " + uuid_string;
        buttonText.text = "Lock";
        savedStatusText.text = "Not Saved";

        buttonText.color = Color.blue;
    }


    // 앵커 세이브 함수
    public void SaveAnchor()
    {
        if (uuid_string == null)
            return;

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
        PlayerPrefs.SetString("uuid" + playerNumUuids, uuid_string);
        PlayerPrefs.SetString(uuid_string, gameObject.name);
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
        PlayerPrefs.Save();

        saveText.color = Color.red;
        unSaveText.color = Color.white;
    }


    // 앵커 세이브 해제 함수
    public void UnsaveAnchor()
    {
        spatialAnchor.Erase((erasedAnchor, success) =>
        {
            if (success)
            {
                savedStatusText.text = "Not Saved";
            }
        });

        DeleteUuidByValue(uuid_string);
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

                    saveText.color = Color.white;
                    unSaveText.color = Color.red;
                    uuid_string = null;

                    if (isRemove)
                    {
                        Destroy(spatialAnchor);
                        Destroy(gameObject);
                    }

                    break;
                }
            }
        }
        else
        {
            Destroy(spatialAnchor);
            Destroy(gameObject);
        }
    }


    // 오브젝트 삭제
    public void DeleteObject() 
    {
        isRemove = true;
        DeleteUuidByValue(uuid_string);
    }

}
