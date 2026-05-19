using Mirror;
using UnityEngine;
using Valve.VR;

public class VRPlayerSetup : NetworkBehaviour
{
    [Header("Components to disable for remote players")]
    public Camera vrCamera;
    public SteamVR_PlayArea playArea; // if you have one

    public override void OnStartLocalPlayer()
    {
        // This runs only on YOUR instance of the prefab
        vrCamera.enabled = true;
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            // Disable camera on the remote player's rig
            vrCamera.enabled = false;
            if (playArea != null) playArea.enabled = false;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isLocalPlayer)
        {
            // Disable SteamVR tracking on remote rig
            GetComponentInChildren<SteamVR_PlayArea>()?.gameObject.SetActive(false);

            foreach (var tracked in GetComponentsInChildren<SteamVR_Behaviour_Pose>())
                tracked.enabled = false;

            foreach (var hand in GetComponentsInChildren<SteamVR_Behaviour_Skeleton>())
                hand.enabled = false;
        }
    }
}