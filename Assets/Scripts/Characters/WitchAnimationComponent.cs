using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimationComponent : MonoBehaviour
{
    public PlayerController player;
    public Animator animator;
    public GameObject aimSpine;
    public GameObject lookAtHead;
    public GameObject hand;
    public ParticleSystem dashParticles;

    private void Start()
    {
        player.damageEvent += Flinch;
        player.deathEvent += Death;
        player.Subscribe(this);
    }
    
    public void DashStart()
    {
        var emission = dashParticles.emission;
        emission.enabled = true;
    }

    public void DashEnd()
    {
        var emission = dashParticles.emission;
        emission.enabled = false;
    }

    public void ChargeStart()
    {
        animator.ResetTrigger("Cancel");
        animator.ResetTrigger("ChargeEnd");
        animator.SetTrigger("ChargeStart");
        animator.SetBool("Charging", true);
    }

    public void ChargeEnd()
    {
        animator.ResetTrigger("Cancel");
        animator.SetTrigger("ChargeEnd");
        animator.SetBool("Charging", false);
    }

    public void Attack()
    {
        animator.ResetTrigger("Cancel");
        animator.SetTrigger("Attack");
        animator.SetBool("Charging", false);
    }

    public void Cancel()
    {
        animator.SetTrigger("Cancel");
        animator.SetBool("Charging", false);
    }
    public void Drink()
    {
        animator.ResetTrigger("Cancel");
        animator.SetTrigger("Drink");
    }

    public void onehanded_Draw()
    {
        animator.ResetTrigger("Cancel");
        animator.SetTrigger("Onehanded");
    }

    public void long_Draw()
    {
        animator.ResetTrigger("Cancel");
        animator.SetTrigger("Long");
    }

    public void Unarmed()
    {
        animator.SetLayerWeight(1, 0f);
    }

    public void Hold()
    {
        animator.SetBool("Charging", false);
        animator.SetLayerWeight(1, 1f);
        player.holding.holdObject.transform.SetParent(hand.transform);
        player.holding.holdObject.transform.localRotation = Quaternion.identity;
        player.holding.holdObject.transform.localPosition = Vector3.zero;
    }

    public void Death(Damage damage)
    {
        animator.SetLayerWeight(1, 0f);
        animator.SetTrigger("Die");
    }
    public void Flinch(Damage damage)
    {
        if (!player.IsAlive())
            return;
        var flinchAnim = Random.Range(0, 2);
        switch(flinchAnim)
        {
            case 0:
                animator.SetTrigger("Flinch 1");
                break;
            case 1:
                animator.SetTrigger("Flinch 2");
                break;
        }
    }

    void LateUpdate()
    {
        if (!player.IsAlive())
            return;

        var oldHeadRotation = lookAtHead.transform.rotation;
        lookAtHead.transform.LookAt(player.lookAtTarget);
        lookAtHead.transform.rotation = Quaternion.Slerp(oldHeadRotation, lookAtHead.transform.rotation, player.lookAtIntensity);
        
        var aim = player.GetAim();
        var targetAim = new Vector3(aim.x, aimSpine.transform.position.y, aim.z);
        aimSpine.transform.LookAt(targetAim);

        var vel = player.GetRigidBody().velocity;
        var movement = new Vector3(vel.x,0f,vel.z);
        if (movement.magnitude > Vector3.kEpsilon)
        {
            animator.SetBool("Moving", true);
            if (player.backwards)
                animator.SetFloat("Movement", -1f);
            else
                animator.SetFloat("Movement", 1f);
        }
        else
            animator.SetBool("Moving", false);
    }

}
