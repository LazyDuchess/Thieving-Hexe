using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyAnimationComponent : MonoBehaviour
{
    public SkellyComponent skelly;
    public Animator animator;
    public GameObject lookAtHead;

    private void Start()
    {
        skelly.damageEvent += Flinch;
        skelly.Subscribe(this);
    }

    public void Melee()
    {
        if (!skelly.IsAlive())
            return;
        var atkAnim = Random.Range(0, 2);
        switch (atkAnim)
        {
            case 0:
                animator.SetTrigger("Attack 1");
                break;
            case 1:
                animator.SetTrigger("Attack 2");
                break;
        }
    }

    private void Update()
    {
        if (skelly.FlatVelocity().magnitude > Vector3.kEpsilon)
            animator.SetBool("Moving",true);
        else
            animator.SetBool("Moving", false);
    }

    private void LateUpdate()
    {
        var oldHeadRotation = lookAtHead.transform.rotation;
        lookAtHead.transform.LookAt(skelly.lookAtTarget);
        lookAtHead.transform.rotation = Quaternion.Slerp(oldHeadRotation, lookAtHead.transform.rotation, skelly.lookAtIntensity);
    }

    public void Flinch(Damage damage)
    {
        if (!skelly.IsAlive())
            return;
        animator.SetTrigger("Flinch");
    }
}