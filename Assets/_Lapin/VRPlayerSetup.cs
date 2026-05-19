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
}