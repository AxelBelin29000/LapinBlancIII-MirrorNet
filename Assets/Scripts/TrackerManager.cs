using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[ExecuteAlways]
public class TrackerManager : MonoBehaviour
{
    [System.Serializable]
    public class TrackerAssignment
    {
        public GameObject[] trackerObjects;
        public int deviceIndex;
    }

    public TrackerAssignment[] trackers;

    void Start()
    {
        AssignDevices();
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            AssignDevices();
        }
    }

    void OnValidate()
    {
        AssignDevices();
    }

    void AssignDevices()
    {
        if (trackers == null)
            return;

        foreach (var tracker in trackers)
        {
            if (tracker.trackerObjects == null)
                continue;

            foreach (var trackerObject in tracker.trackerObjects)
            {
                if (trackerObject == null)
                    continue;

                SteamVR_TrackedObject tracked = trackerObject.GetComponent<SteamVR_TrackedObject>();

                if (tracked != null)
                {
                    tracked.SetDeviceIndex(tracker.deviceIndex);
                }
            }
        }
    }
}