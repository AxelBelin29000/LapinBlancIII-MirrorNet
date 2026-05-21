using Mirror;
using System.Collections;
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
        if (leftRenderModel != null) leftRenderModel.gameObject.SetActive(newVal);
    }

    void OnRightTrackedChanged(bool oldVal, bool newVal)
    {
        rightProxy.gameObject.SetActive(newVal);
        if (rightRenderModel != null) rightRenderModel.gameObject.SetActive(newVal);
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        headProxy.position = vrCamera.transform.position;
        headProxy.rotation = vrCamera.transform.rotation;

        bool isLeftTracked = leftPose != null && leftPose.isValid;
        bool isRightTracked = rightPose != null && rightPose.isValid;

        Debug.Log($"Left: {isLeftTracked} Right: {isRightTracked}");

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
        if (leftRenderModel != null) leftRenderModel.gameObject.SetActive(isLeftTracked);
        if (rightRenderModel != null) rightRenderModel.gameObject.SetActive(isRightTracked);
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

        Debug.Log($"Left render model found: {leftRenderModel != null}");
        Debug.Log($"Right render model found: {rightRenderModel != null}");

        StartCoroutine(InitialStateCheck());
    }

    IEnumerator InitialStateCheck()
    {
        // wait for SteamVR to report correct tracking state
        yield return new WaitForSeconds(1f);
        CmdSetLeftTracked(leftPose != null && leftPose.isValid);
        CmdSetRightTracked(rightPose != null && rightPose.isValid);
    }
}