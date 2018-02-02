using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
	public static float Radius = 40;

	public static Vector3 ProjectPosition( Vector3 pos )
	{
		Vector3 origin = new Vector3(0, pos.y, 0);
		Vector3 dir = pos - origin;
		Vector3 projectedPos = origin + dir.normalized * Radius;
		return projectedPos;
	}
		
	public static Vector3 GetPointToYAxisDirection( Vector3 pos )
	{
		Vector3 origin = new Vector3(0, pos.y, 0);
		return (origin - pos).normalized;
	}

	public static Vector3 ProjectPosition( Vector3 pos, float radius )
	{
		Vector3 origin = new Vector3(0, pos.y, 0);
		Vector3 dir = pos - origin;
		Vector3 projectedPos = origin + dir.normalized * radius;
		return projectedPos;
	}

    /// <summary>
    /// transform the a direciton from local system to world system
    /// The local system is defined by the system that the z coorindate is always outwards the cylinder center axis, y coordinate
    /// is always up , and x from y and z using left hand rule(unity's rule)
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 TransformDirection(Vector3 coordinateCenter, Vector3 directionToTransform)
    {
        Matrix4x4 rotMatrix = new Matrix4x4();
        Vector3 y = Vector3.up;
        Vector3 z = coordinateCenter;
        z.y = 0;
        z.Normalize();
        Vector3 x = Vector3.Cross(y, z);
        rotMatrix.SetColumn(0, new Vector4(x.x,x.y,x.z,0));
        rotMatrix.SetColumn(1, new Vector4(y.x, y.y, y.z, 0));
        rotMatrix.SetColumn(2, new Vector4(z.x, z.y, z.z, 0));
        rotMatrix.SetColumn(3, new Vector4(0, 0, 0, 1));
        return rotMatrix.MultiplyVector(directionToTransform);
    }

}
