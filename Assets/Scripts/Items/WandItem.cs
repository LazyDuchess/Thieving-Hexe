using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandItem : ItemComponent
{
    public GameObject projectilePrefab;
    public float velocity = 5f;
    public float offset = 1f;
    public float coolDown = 0.6f;
    public float coolDownCharge = 1f;
    public float heightOffset = -0.1f;
    public float speedBuff = 0.9f;
    public float delay = 0.1f;
    public float duration = 0.2f;
    public override void Primary()
    {
        base.Primary();
        var attackAction = new ActionWandPrimary(owner, this, velocity, offset, heightOffset, speedBuff, duration, delay, projectilePrefab);
        owner.QueueAction(attackAction);
        GameEventsController.PlayerAttack();
        /*
        owner.SendEvent("Attack");
        var aimHeading = (owner as PlayerController).GetAimHeadingFlat();
        var pos = owner.transform.position + (Vector3.up * owner.height) + (aimHeading * offset) + (Vector3.up * heightOffset);
        var proj = Instantiate(projectilePrefab,pos,Quaternion.identity);
        var pro = proj.GetComponent<Projectile>();
        pro.owner = this.owner;
        pro.vector = aimHeading * velocity;*/
        CoolDownPrimary(coolDown);
        CoolDownSecondary(coolDown);
    }

    public override void Secondary()
    {
        base.Secondary();
        owner.SendEvent("ChargeStart");
        GameEventsController.PlayerChargedAttackStart();
    }

    public override void SecondaryEnd()
    {
        base.SecondaryEnd();
        owner.SendEvent("ChargeEnd");
        CoolDownPrimary(coolDown);
        CoolDownSecondary(coolDownCharge);
        var aimHeading = (owner as PlayerController).GetAimHeadingFlat();
        var pos = owner.transform.position + (Vector3.up * owner.height) + (aimHeading * offset);
        var proj = Instantiate(projectilePrefab, pos, Quaternion.identity);
        var pro = proj.GetComponent<Projectile>();
        pro.owner = this.owner;
        pro.vector = aimHeading * velocity;
        GameEventsController.PlayerChargedAttackEnd();
    }
}
