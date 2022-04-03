using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class VectorUtil
{
    public static Vector3 RandomNormal()
    {
        var radius = 32f;
        var randomVec = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
        return randomVec.normalized;
    }
}
