using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchItem : ItemComponent
{
    public float coolDown = 1.0f;
    public float speedBuff = 0.5f;
    public float damage = 15f;
    public float duration = 0.7f;
    public float radius = 1.0f;
    public float distance = 0.7f;
    public float pushForce = 2f;
    public float delay = 0.2f;
    public override void Primary()
    {
        base.Primary();
        //var attackAction = new ActionWandPrimary(owner, this, velocity, offset, heightOffset, speedBuff, duration, delay, projectilePrefab);
        var attackAction = new MeleeAttackAction(owner, distance, radius, duration, damage, true, speedBuff, pushForce, delay);
        owner.QueueAction(attackAction);
        GameEventsController.PlayerMeleeAttack();
        CoolDownPrimary(coolDown);
        CoolDownSecondary(coolDown);
    }
}
