using UnityEngine;
using System.Collections;

public static class Vector3Extension
{
    /// <summary>
    /// Rotates this vector around a given pivot the given
    /// number of degrees on each axis.
    /// </summary>
    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    public static Vector3 Signs(this Vector3 vector)
    {
        return new Vector3(Mathf.Sign(vector.x), Mathf.Sign(vector.y), Mathf.Sign(vector.z));
    }
}

