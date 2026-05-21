using UnityEngine;
using UnityEngine.Splines;

public class KnotFollower : MonoBehaviour
{
    public SplineContainer splineContainer;

    // index du knot � d�placer
    public int knotIndex = 0;

    // objet qui sert de r�f�rence
    public Transform targetObject;

    void Update()
    {
        Spline spline = splineContainer.Spline;

        BezierKnot knot = spline[knotIndex];

        // convertir la position monde en local spline
        Vector3 localPos =
            splineContainer.transform.InverseTransformPoint(
                targetObject.position
    
            );
        

        knot.Position = localPos;
      

        spline[knotIndex] = knot;
    }
}
