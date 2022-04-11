using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInfo
{
    public Vector3 origin;
    public Vector3 heading;
    public float distance;
}
public static class VectorUtil
{
    public static RayInfo MakeRay(Vector3 fromPoint, Vector3 toPoint, float offset)
    {
        var off = offset;
        var rayOrigin = fromPoint;
        var rayHeading = (toPoint - fromPoint).normalized;
        var rayDistance = Vector3.Distance(fromPoint, toPoint);
        rayOrigin += rayHeading * off;
        rayDistance -= off * 2f;
        //Ray ray = new Ray(rayOrigin, rayHeading);
        Debug.DrawLine(rayOrigin, rayOrigin + (rayDistance * rayHeading), Color.red, 10f);
        var returnRay = new RayInfo();
        returnRay.origin = rayOrigin;
        returnRay.heading = rayHeading;
        returnRay.distance = rayDistance;
        return returnRay;
    }
    public static Vector3 RandomNormal()
    {
        var radius = 32f;
        var randomVec = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
        return randomVec.normalized;
    }
}
