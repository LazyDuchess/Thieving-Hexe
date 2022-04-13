using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAction : Action
{
    float distance;
    float radius;
    float timeRemaining;
    float damage;
    float duration;
    float pushForce;
    float delay;
    public MeleeAttackAction(CharacterComponent owner, float distance, float radius, float duration, float damage, bool movable, float speedBuff = 1f, float pushForce = 0f, float delay = 0f) : base(owner)
    {
        this.owner = owner;
        this.distance = distance;
        this.radius = radius;
        this.duration = duration;
        this.damage = damage;
        this.movable = movable;
        this.useInventory = false;
        this.speedBuff = speedBuff;
        this.pushForce = pushForce;
        this.delay = delay;
    }

    public override void Enter()
    {
        timeRemaining = duration;
        owner.SendEvent("Attack");
        QueueEvent(delay, 0);
    }

    public override void Event(int id)
    {
        base.Event(id);
        var hits = HitUtils.Hit(owner, owner.transform.position + (owner.GetRotation() * Vector3.forward * distance) + (owner.height * Vector3.up), radius);
        foreach (var element in hits)
        {
            element.entity.TakeDamage(new Damage()
            {
                hp = damage,
                vector = owner.transform.position,
                pushForce = this.pushForce
            });
        }
    }

    public override bool Tick()
    {
        base.Tick();
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
            return true;
        else
            return false;
    }
}
