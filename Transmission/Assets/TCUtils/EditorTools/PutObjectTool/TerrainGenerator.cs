using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour {

    
    public bool placingObjectEnable;
    public BrushMode brushMode = BrushMode.FixedRadius;
    public GameObject[] objectsPrefs;
    public Transform parentTransform;

    public Vector2 gizmoVerticalRange = new Vector2(-500, 100);
    public LayerMask collisionLayer = ~0;
    [Header("Put Object Options")]
    public float autoPuttineTimeInterval = 0.1f;
    public float radiusToPutObject = 20;
    public Vector2 scaleRange = Vector2.one;
    public bool randomRotation = true;
    public Vector3 initialRotation;
    public Vector3 offsetForSurfaceMode = Vector3.zero;
    public enum BrushMode
    {
        FixedRadius,
        OnSurface,
        OnSurfaceAuto
    }


    public Ray MouseRay { get; set; }
    List<Vector3> gizmoCylinerPoints;
    private readonly float DesriedCylinderPointsInterval = 3.0f;

    
    private float prevRadiusToPutObject;

    public GameObject CurrentObject { get; private set; }

    private Quaternion initialRotationQuaternion;

    private float autoPuttingTimer;
    private bool mouseDown = false;
    // Use this for initialization
    void Start () {
        CurrentObject = null;
        autoPuttingTimer = 0;
    }
	

    public void UpdatePuttingObject()
    {
        if (prevRadiusToPutObject != radiusToPutObject || gizmoCylinerPoints == null)
        {
            ReconstructGizmoPoints();
        }
        else if (placingObjectEnable)
        {
            if (brushMode == BrushMode.FixedRadius)
            {
                PlacingObjectRadius();
            }
            else if(brushMode == BrushMode.OnSurface )
            {
                PlacingObjectSurface();
            }else if(brushMode == BrushMode.OnSurfaceAuto)
            {
                if(mouseDown)
                    CountingDownPuttingTimer();
                PlacingObjectSurface();
            }
        }
        else if (CurrentObject != null)
        {
            GameObject.DestroyImmediate(CurrentObject);
        }
       
        prevRadiusToPutObject = radiusToPutObject;
        //print("Updated");
    }

    //should be called by the editor script, pass the created object to this function and thiss function will place the object in the correct position
    public void AddNewObject(GameObject obj)
    {
        CurrentObject = obj;
        if (randomRotation)
        {
            initialRotationQuaternion = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
            initialRotation = initialRotationQuaternion.eulerAngles;
        }
        else
        {
            initialRotationQuaternion = Quaternion.Euler(initialRotation.x, initialRotation.y, initialRotation.z);
        }
        CurrentObject.transform.SetParent(parentTransform);
        CurrentObject.transform.rotation = initialRotationQuaternion;
        CurrentObject.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
    }

    public void OnMouseDown()
    {
        CurrentObject = null;
        if(placingObjectEnable)
            mouseDown = true;
        autoPuttingTimer = autoPuttineTimeInterval;
    }
    public void OnMouseUp()
    {
        mouseDown = false;
    }
    //putting the object  on a certain radius
    private void PlacingObjectRadius()
    {
        if(CurrentObject == null)
        {
            return;
        }
        ConstrainToCylinder constraint = CurrentObject.GetComponent<ConstrainToCylinder>();
        if(constraint != null)
        {
            constraint.enabled = false;
        }

        Vector3 point = FindPlacementPointOnRadius(MouseRay, radiusToPutObject);
        CurrentObject.transform.position = point;

        //update the rotation

        initialRotationQuaternion = Quaternion.Euler(initialRotation.x, initialRotation.y, initialRotation.z);
        
        Vector3 center = Vector3.zero;
        center.y = CurrentObject.transform.position.y;
        Quaternion rotLook = Quaternion.LookRotation((center - CurrentObject.transform.position).normalized);
        CurrentObject.transform.rotation = rotLook * initialRotationQuaternion;

    }

    //putting the object  on a the pointed surface
    private void PlacingObjectSurface()
    {
        if (CurrentObject == null)
        {
            return;
        }
        ConstrainToCylinder constraint = CurrentObject.GetComponent<ConstrainToCylinder>();
        if (constraint != null)
        {
            constraint.enabled = false;
        }

        Vector3 point, normal;

        FindPlacementPointOnSurface(MouseRay, collisionLayer,out point, out normal);
        CurrentObject.transform.position = point;

        //update the rotation
        Quaternion currentRot = CurrentObject.transform.rotation;
        Vector3 currentY = CurrentObject.transform.TransformDirection(Vector3.up);
        
        //Matrix4x4 basis = Utils.FormBasis(normal);
        //Quaternion rot = Quaternion.LookRotation(basis.GetColumn(0), basis.GetColumn(2));
        CurrentObject.transform.rotation = Quaternion.FromToRotation(currentY, normal) * CurrentObject.transform.rotation;
        CurrentObject.transform.position = CurrentObject.transform.TransformPoint(offsetForSurfaceMode);

    }

    private void CountingDownPuttingTimer()
    {
        autoPuttingTimer -= Time.deltaTime;
        if(autoPuttingTimer <= 0)
        {
            autoPuttingTimer = autoPuttineTimeInterval;
            CurrentObject = null;
        }
    }

    private void ReconstructGizmoPoints()
    {
        if ( gizmoCylinerPoints == null)
        {
            gizmoCylinerPoints = new List<Vector3>();
        }
        int numOfPoints = (int)(radiusToPutObject * Mathf.PI * 2 / DesriedCylinderPointsInterval);
        float actualCylinderPointsAngle = (Mathf.PI * 2) / numOfPoints;
        gizmoCylinerPoints.Clear();
        for(int i = 0; i < numOfPoints; ++i)
        {
            Vector3 point = new Vector3(Mathf.Sin(actualCylinderPointsAngle*i) * radiusToPutObject, 
                0,
                Mathf.Cos(actualCylinderPointsAngle*i) * radiusToPutObject);
            gizmoCylinerPoints.Add(point);
        }
    }

    //find a point on the cylinder of certain radius that is closest to the ray
    private Vector3 FindPlacementPointOnRadius(Ray ray, float radius)
    {
        Vector3 dir = ray.direction;
        Vector3 orig = ray.origin;
        orig.y = 0;
        dir.y = 0;

        Vector3 closestPoint = FindClosestPoint(orig, dir, Vector3.zero); 
        if(closestPoint.magnitude >= radius)
        {
            Vector3 temp = ray.origin + ((closestPoint - orig).x / ray.direction.x) * ray.direction;
            Vector3 result = temp;
            result.y = 0;
            result = Vector3.ClampMagnitude(result, radius);
            result.y = temp.y;
            return result;

        }
        else
        {
            float length = Mathf.Sqrt(radius * radius - closestPoint.magnitude * closestPoint.magnitude);
            Vector3 pos = closestPoint + dir.normalized * length;

            Vector3 result = ray.origin + ((pos - orig).x / ray.direction.x) * ray.direction;
            return result;
        }
    }
    
    //return the closest point on the ray to the point
    private Vector3 FindClosestPoint(Vector3 rayOrig, Vector3 rayDir, Vector3 point)
    {
        Vector3 result = rayDir.normalized * Vector3.Dot(rayDir.normalized, point - rayOrig) + rayOrig;
        return result;
    }


    private void FindPlacementPointOnSurface(Ray ray, LayerMask collisionMask, out Vector3 position, out Vector3 normal)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            position = hit.point;
            normal = hit.normal;
        }
        else
        {
            position = ray.origin + ray.direction * 20;
            normal = Vector3.up;
        }
    }






    void OnDrawGizmos()
    {
        if (brushMode == BrushMode.FixedRadius)
        {
            Gizmos.color = new Color(0.2f, 0.91f, 0.98f);
            if (gizmoCylinerPoints != null && placingObjectEnable)
            {
                int num = (int)((gizmoVerticalRange.y - gizmoVerticalRange.x) / 10.0f);
                for (int j = 0; j < num; ++j)
                {

                    for (int i = 1; i < gizmoCylinerPoints.Count; ++i)
                    {
                        Gizmos.DrawLine(gizmoCylinerPoints[i - 1] + Vector3.up * (gizmoVerticalRange.y - 10 * j),
                            gizmoCylinerPoints[i] + Vector3.up * (gizmoVerticalRange.y - 10 * j));
                    }
                    Gizmos.DrawLine(gizmoCylinerPoints[gizmoCylinerPoints.Count - 1] + Vector3.up * (gizmoVerticalRange.y - 10 * j),
                        gizmoCylinerPoints[0] + Vector3.up * (gizmoVerticalRange.y - 10 * j));
                }
            }
        }
    }

}
