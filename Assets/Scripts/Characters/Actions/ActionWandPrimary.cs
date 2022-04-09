using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWandPrimary : Action
{
    bool done = false;
    float duration = 2f;
    ItemComponent weapon;
    float delay = 0f;
    float offset = 0f;
    float heightOffset = 0f;
    GameObject projectilePrefab;
    float velocity;
    public ActionWandPrimary(CharacterComponent owner, ItemComponent weapon, float velocity, float offset, float heightOffset, float speedBuff, float duration, float delay, GameObject projectilePrefab) : base(owner)
    {
        this.owner = owner;
        this.movable = true;
        this.interruptible = true;
        this.priority = ActionPriority.Base;
        this.speedBuff = speedBuff;
        this.useInventory = false;
        this.duration = duration;
        this.weapon = weapon;
        this.delay = delay;
        this.offset = offset;
        this.heightOffset = heightOffset;
        this.projectilePrefab = projectilePrefab;
        this.velocity = velocity;
    }

    public override void Enter()
    {
        base.Enter();
        owner.SendEvent("Attack");
        QueueEvent(delay, 0);
        QueueEvent(duration, 1);
    }

    public override void Event(int id)
    {
        switch (id)
        {
            case 0:
               
                var aimHeading = (owner as PlayerController).GetAimHeadingFlat();
                var pos = owner.transform.position + (Vector3.up * owner.height) + (aimHeading * offset) + (Vector3.up * heightOffset);
                var proj = GameObject.Instantiate(projectilePrefab, pos, Quaternion.identity);
                var pro = proj.GetComponent<Projectile>();
                pro.owner = this.owner;
                pro.vector = aimHeading * velocity;
                break;
            case 1:
                done = true;
                break;
        }
    }

    public override bool Tick()
    {
        base.Tick();
        if (done)
            return true;
        return false;
    }
}
