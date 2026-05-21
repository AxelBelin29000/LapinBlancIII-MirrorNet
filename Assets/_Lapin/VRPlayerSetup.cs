using Mirror;
using UnityEngine;
using Valve.VR;

public class VRPlayerSetup : NetworkBehaviour
{
    public GameObject vrCamera;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public Transform headProxy;
    public Transform leftProxy;
    public Transform rightProxy;
    public SteamVR_Behaviour_Pose leftPose;
    public SteamVR_Behaviour_Pose rightPose;
    private SteamVR_RenderModel leftRenderModel;
    private SteamVR_RenderModel rightRenderModel;

    [SyncVar(hook = nameof(OnLeftTrackedChanged))]
    bool leftTracked;

    [SyncVar(hook = nameof(OnRightTrackedChanged))]
    bool rightTracked;

    void OnLeftTrackedChanged(bool oldVal, bool newVal)
    {
        leftProxy.gameObject.SetActive(newVal);
        if (leftRenderModel != null) leftRenderModel.enabled = newVal;
    }

    void OnRightTrackedChanged(bool oldVal, bool newVal)
    {
        rightProxy.gameObject.SetActive(newVal);
        if (rightRenderModel != null) rightRenderModel.enabled = newVal;
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        headProxy.position = vrCamera.transform.position;
        headProxy.rotation = vrCamera.transform.rotation;

        bool isLeftTracked = leftPose != null && leftPose.isValid;
        bool isRightTracked = rightPose != null && rightPose.isValid;

        if (isLeftTracked != leftTracked)
            CmdSetLeftTracked(isLeftTracked);

        if (isRightTracked != rightTracked)
            CmdSetRightTracked(isRightTracked);

        if (isLeftTracked)
        {
            leftProxy.position = controllerLeft.transform.position;
            leftProxy.rotation = controllerLeft.transform.rotation;
        }

        if (isRightTracked)
        {
            rightProxy.position = controllerRight.transform.position;
            rightProxy.rotation = controllerRight.transform.rotation;
        }

        // show/hide SteamVR render model locally
        if (leftRenderModel != null) leftRenderModel.enabled = isLeftTracked;
        if (rightRenderModel != null) rightRenderModel.enabled = isRightTracked;
    }

    [Command]
    void CmdSetLeftTracked(bool value) => leftTracked = value;

    [Command]
    void CmdSetRightTracked(bool value) => rightTracked = value;

    public override void OnStartLocalPlayer()
    {
        vrCamera.SetActive(true);
        controllerLeft.SetActive(true);
        controllerRight.SetActive(true);

        leftRenderModel = controllerLeft.GetComponentInChildren<SteamVR_RenderModel>();
        rightRenderModel = controllerRight.GetComponentInChildren<SteamVR_RenderModel>();

        // force initial state
        CmdSetLeftTracked(leftPose != null && leftPose.isValid);
        CmdSetRightTracked(rightPose != null && rightPose.isValid);
    }
}