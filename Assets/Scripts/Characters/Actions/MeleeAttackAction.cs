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
    public MeleeAttackAction(CharacterComponent owner, float distance, float radius, float duration, float damage, bool movable) : base(owner)
    {
        this.owner = owner;
        this.distance = distance;
        this.radius = radius;
        this.duration = duration;
        this.damage = damage;
        this.movable = movable;
    }

    public override void Enter()
    {
        timeRemaining = duration;
        owner.SendEvent("Melee");
        var hits = HitUtils.Hit(owner.GetTeam(), owner.transform.position + (owner.GetRotation() * Vector3.forward * distance) + (owner.height * Vector3.up), radius);
        foreach(var element in hits)
        {
            element.entity.TakeDamage(new Damage()
            {
                hp = damage
            }) ;
        }
    }

    public override bool Tick()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
            return true;
        else
            return false;
    }
}
