using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gibs : MonoBehaviour
{
    public float strength = 2f;
    private void Start()
    {
        var bodies = GetComponentsInChildren<Rigidbody>();
        foreach(var element in bodies)
        {
            var throwVector = VectorUtil.RandomNormal() * strength;
            element.AddForce(throwVector, ForceMode.Impulse);
        }
    }
}