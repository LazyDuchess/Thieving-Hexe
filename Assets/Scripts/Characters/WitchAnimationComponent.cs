using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimationComponent : MonoBehaviour
{
    public PlayerController player;
    public Animator animator;
    public GameObject aimSpine;
    public GameObject lookAtHead;

    public A_Player playerAudio;


    private void Start()
    {
        player.damageEvent += Flinch;
        player.deathEvent += Death;
  
    }

    public void Death(Damage damage)
    {
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
                playerAudio.PlayTakeDamage();
                break;
            case 1:
                animator.SetTrigger("Flinch 2");
                playerAudio.PlayTakeDamage();
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
