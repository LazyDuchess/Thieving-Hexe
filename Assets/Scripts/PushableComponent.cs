using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableComponent : MonoBehaviour
{
    public float radius = 0.1f;
    public float pushMultiplier = 0.5f;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        var hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var element in hits)
        {
            var charComp = element.GetComponent<CharacterComponent>();
            if (charComp)
            {
                if (charComp.GetRigidBody().velocity.magnitude > Vector3.kEpsilon)
                {

                    rigidBody.AddForceAtPosition(charComp.GetRigidBody().velocity * pushMultiplier, charComp.transform.position, ForceMode.Force);
                }
            }
        }
    }
}
