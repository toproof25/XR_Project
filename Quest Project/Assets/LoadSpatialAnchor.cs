using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public class LoadSpatialAnchor : MonoBehaviour
{

    public OVRSpatialAnchor anchorPrefab;
    

    Action<OVRSpatialAnchor.UnboundAnchor, bool> _onLoadAnchor;



    private void Awake()
    {
        _onLoadAnchor = OnLocalized;
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

        var pose = unboundAnchor.Pose;
        var spatialAnchor = Instantiate(anchorPrefab, pose.position, pose.rotation);
        unboundAnchor.BindTo(spatialAnchor);

        if(spatialAnchor.TryGetComponent<OVRSpatialAnchor>(out var anchor)) 
        {
            var uuidText = spatialAnchor.GetComponentInChildren<TextMeshProUGUI>();
            //var savedStatusText = spatialAnchor.GetComponentInChildren<TextMeshProUGUI>();

            uuidText.text = "UUID: " + spatialAnchor.Uuid.ToString();
        }
    }

}
