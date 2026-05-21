using Mirror;
using UnityEngine;

public class PuppetSync : NetworkBehaviour
{
    [Header("Trackers (host side)")]
    public Transform trackerHead;
    public Transform trackerMouth;
    public Transform trackerLegLeft;
    public Transform trackerLegRight;

    [Header("Puppet bones")]
    public Transform boneHead;
    public Transform boneMouth;
    public Transform boneLegLeft;
    public Transform boneLegRight;

    [SyncVar] Vector3 headPos;
    [SyncVar] Quaternion headRot;
    [SyncVar] Vector3 mouthPos;
    [SyncVar] Quaternion mouthRot;
    [SyncVar] Vector3 legLeftPos;
    [SyncVar] Quaternion legLeftRot;
    [SyncVar] Vector3 legRightPos;
    [SyncVar] Quaternion legRightRot;

    void Update()
    {
        if (isServer)
        {
            // read trackers and push to SyncVars
            headPos = trackerHead.position;
            headRot = trackerHead.rotation;
            mouthPos = trackerMouth.position;
            mouthRot = trackerMouth.rotation;
            legLeftPos = trackerLegLeft.position;
            legLeftRot = trackerLegLeft.rotation;
            legRightPos = trackerLegRight.position;
            legRightRot = trackerLegRight.rotation;
        }

        // apply to bones on all clients including host
        boneHead.position = headPos;
        boneHead.rotation = headRot;
        boneMouth.position = mouthPos;
        boneMouth.rotation = mouthRot;
        boneLegLeft.position = legLeftPos;
        boneLegLeft.rotation = legLeftRot;
        boneLegRight.position = legRightPos;
        boneLegRight.rotation = legRightRot;
    }
}