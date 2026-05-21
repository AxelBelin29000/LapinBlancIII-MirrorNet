
using UnityEngine;

[System.Serializable]
public class TransformTarget
{
    public Transform target;

    [Header("Position Settings")]
    public bool affectPosition = true;
    public Vector3 positionFactor = Vector3.one;
    public float positionThreshold = 0.01f;

    [Header("Rotation Settings")]
    public bool affectRotation = true;
    public Vector3 rotationFactor = Vector3.one;
    public float rotationThreshold = 0.5f;
}

public class TransformPropagator : MonoBehaviour
{
    [Header("Targets")]
    public TransformTarget[] targets;

    [Header("Anti Jitter")]
    [Range(1f, 30f)]
    public float positionSmooth = 18f;

    [Range(1f, 30f)]
    public float rotationSmooth = 18f;

    [Header("Global Deadzone")]
    public float globalPositionDeadzone = 0.001f;
    public float globalRotationDeadzone = 0.1f;

    private Vector3 filteredPosition;
    private Vector3 filteredRotation;

    private Vector3 lastPosition;
    private Vector3 lastRotation;

    void Start()
    {
        filteredPosition = transform.position;
        //filteredRotation = transform.eulerAngles;

        lastPosition = filteredPosition;
        //lastRotation = filteredRotation;
    }

    void LateUpdate()
    {
        // -------------------------
        // FILTRAGE POSITION
        // -------------------------

        Vector3 rawPosition = transform.position;

        if (Vector3.Distance(filteredPosition, rawPosition) > globalPositionDeadzone)
        {
            filteredPosition = Vector3.Lerp(
                filteredPosition,
                rawPosition,
                Time.deltaTime * positionSmooth
            );
        }

        // -------------------------
        // FILTRAGE ROTATION
        // -------------------------
/*
        Vector3 rawRotation = transform.eulerAngles;

        if (Vector3.Distance(filteredRotation, rawRotation) > globalRotationDeadzone)
        {
            filteredRotation = Vector3.Lerp(
                filteredRotation,
                rawRotation,
                Time.deltaTime * rotationSmooth
            );
        }
*/
        // -------------------------
        // DELTAS FILTRÉS
        // -------------------------

        Vector3 positionDelta = filteredPosition - lastPosition;
        //Vector3 rotationDelta = filteredRotation - lastRotation;

        foreach (var t in targets)
        {
            if (t.target == null)
                continue;

            // ----- POSITION -----

            if (t.affectPosition &&
                positionDelta.magnitude > t.positionThreshold)
            {
                Vector3 modifiedDelta =
                    Vector3.Scale(positionDelta, t.positionFactor);

                t.target.position += modifiedDelta;
            }

            // ----- ROTATION -----
/*
            if (t.affectRotation &&
                rotationDelta.magnitude > t.rotationThreshold)
            {
                Vector3 modifiedRotation =
                    Vector3.Scale(rotationDelta, t.rotationFactor);

                t.target.eulerAngles += modifiedRotation;
            }*/
        }

        // -------------------------
        // SAVE LAST VALUES
        // -------------------------

        lastPosition = filteredPosition;
        //lastRotation = filteredRotation;
    }
}

