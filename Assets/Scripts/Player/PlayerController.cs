using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterComponent
{
    //For animating
    public bool backwards = false;

    private Vector3 aimLocation = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        DummyAim();
    }

    void DummyAim()
    {
        aimLocation = transform.position + transform.forward * 5f;
    }

    public Vector3 GetAim()
    {
        return aimLocation;
    }

    public void SetAim(Vector3 location)
    {
        aimLocation = location;
    }

    protected override void RotateCharacter()
    {
        return;
    }

    protected override void Update()
    {
        base.Update();
        if (IsAlive())
        {
            Quaternion targetRotation;
            backwards = false;

            var vel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            var targetAim = new Vector3(aimLocation.x, mesh.transform.position.y, aimLocation.z);
            var target = (targetAim - mesh.transform.position).normalized;

            var aimQuaternion = Quaternion.LookRotation(target);
            var movementQuaternion = Quaternion.identity;
            var backwardsMovementQuaternion = Quaternion.identity;

            if (vel != Vector3.zero)
            {
                movementQuaternion = Quaternion.LookRotation(vel.normalized);
                backwardsMovementQuaternion = Quaternion.LookRotation(-vel.normalized);
            }


            var forwardMovement = movementQuaternion * Vector3.forward;

            if (vel.magnitude > Vector3.kEpsilon)
            {
                var heading = Vector3.Dot(forwardMovement, target);
                if (heading < -0.2f)
                {
                    backwards = true;
                    targetRotation = backwardsMovementQuaternion;
                }
                else
                {
                    targetRotation = movementQuaternion;
                }
            }
            else
            {
                targetRotation = aimQuaternion;
            }
            mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, targetRotation, rotationLerp * Time.deltaTime);
        }
    }
}
