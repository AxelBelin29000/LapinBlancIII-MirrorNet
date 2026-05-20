using Mirror;
using UnityEngine;

public class VRPlayerSetup : NetworkBehaviour
{
    public GameObject vrCamera;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public Transform headProxy;      // the sphere, child of root
    public Transform leftProxy;      // visual for left hand
    public Transform rightProxy;     // visual for right hand

    void Update()
    {
        if (!isLocalPlayer) return;
        headProxy.position = vrCamera.transform.position;
        headProxy.rotation = vrCamera.transform.rotation;
        leftProxy.position = controllerLeft.transform.position;
        leftProxy.rotation = controllerLeft.transform.rotation;
        rightProxy.position = controllerRight.transform.position;
        rightProxy.rotation = controllerRight.transform.rotation;
    }

    public override void OnStartLocalPlayer()
    {
        vrCamera.SetActive(true);
        controllerLeft.SetActive(true);
        controllerRight.SetActive(true);
    }
}