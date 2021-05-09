using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DistanceCheckExtension
{
    public static float CheckDistanceY(this Vector3 point1, Vector3 point2)
    {
        var y1 = new Vector3(0, point1.y, 0);
        var y2 = new Vector3(0, point2.y, 0);
        return Vector3.Distance(y1, y2);
    }
}
