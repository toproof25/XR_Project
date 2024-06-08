using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSpatialAnchor : MonoBehaviour
{

    public OVRSpatialAnchor anchorPrefab;
    

    Action<OVRSpatialAnchor.UnboundAnchor, bool> _onLoadAnchor;

    public List<string> prefabs_name = new List<string>(); // 프리팹들 이름
    public List<GameObject> prefabs_model = new List<GameObject>(); // 프리팹들 오브젝트


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        _onLoadAnchor = OnLocalized;
    }

    private void Start()
    {
        //LoadAnchorsBtUuid();
    }

    public void LoadAnchorsBtUuid()
    {
        if (!PlayerPrefs.HasKey("numUuids"))
        {
            PlayerPrefs.SetInt("numUuids", 0);
        }

        var playerUuidCount = PlayerPrefs.GetInt("numUuids");

        if (playerUuidCount == 0)
        {
            return;
        }

        var uuids = new Guid[playerUuidCount];
        for (int i = 0; i < playerUuidCount; ++i)
        {
            var uuidKey = "uuid" + i;
            var currentUuid = PlayerPrefs.GetString(uuidKey);
            if (currentUuid == "")
                continue;
            Debug.Log(playerUuidCount + "     currentUuid-------------------------------------------------------------------------------------------------");
            Debug.Log(currentUuid);
            uuids[i] = new Guid(currentUuid);
        }


        Load(new OVRSpatialAnchor.LoadOptions
        {
            Timeout = 0,
            StorageLocation = OVRSpace.StorageLocation.Local,
            Uuids = uuids
        });
    }

    private void Load(OVRSpatialAnchor.LoadOptions options)
    {
        OVRSpatialAnchor.LoadUnboundAnchors(options, anchors =>
        {
            if (anchors == null)
            {
                return;
            }

            foreach (var anchor in anchors)
            {
                if (anchor.Localized)
                {
                    _onLoadAnchor(anchor, true);
                }
                else if (!anchor.Localizing)
                {
                    anchor.Localize(_onLoadAnchor);
                }
            }
        });
    }

    private void OnLocalized(OVRSpatialAnchor.UnboundAnchor unboundAnchor, bool success) 
    {
        if (!success) return;

        Debug.Log("OnLocalized---OnLocalized--------OnLocalized------OnLocalized------OnLocalized-----OnLocalized---------------------");

        Debug.Log(unboundAnchor.Uuid.ToString());

        // PlayerPrefs에서 저장된 uuid_string을 사용하여 해당하는 GameObject의 이름을 가져옴
        string objectName = PlayerPrefs.GetString(unboundAnchor.Uuid.ToString());

        Debug.Log(objectName + "   :   #@!#@!#@!!@#!@#!@#!#@!#@!@#!@#!@#!@#!@#!@#!@#!@#!@#!@#!@#!@#!@#!@##");

        // 가져온 이름이 prefabs_name 리스트에 있는지 확인
        int index = prefabs_name.IndexOf(objectName);

        GameObject prefabToInstantiate = prefabs_model[index];
        OVRSpatialAnchor prefab = prefabToInstantiate.GetComponent<OVRSpatialAnchor>();

        
        Debug.Log(index);
        Debug.Log(prefabToInstantiate.name);

        var pose = unboundAnchor.Pose;
        var spatialAnchor = Instantiate(prefab, pose.position, pose.rotation);
        unboundAnchor.BindTo(spatialAnchor);

        if(spatialAnchor.TryGetComponent<OVRSpatialAnchor>(out var anchor)) 
        {
            AnchorScript uuid = spatialAnchor.GetComponentInChildren<AnchorScript>();
            uuid.uuid_string = spatialAnchor.Uuid.ToString();

            uuid.uuidText.text = "UUID: " + spatialAnchor.Uuid.ToString();
            uuid.savedStatusText.text = "Save";
            uuid.buttonText.text = "Lock";
            uuid.isAnchored = true;
        }

        Debug.Log("--------------------------------------------------------------OnLocalized--------OnLocalized-----------");
    }



    public void DeletePlayerPrefabs()
    {
        Debug.Log("DeleteAll ---------- ");
        PlayerPrefs.DeleteAll();
    }
}
