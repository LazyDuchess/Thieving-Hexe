using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlinchAction : Action
{
    float currentDuration;
    float duration;
    public FlinchAction(CharacterComponent owner, float duration, bool move, float speedBuff) : base(owner)
    {
        this.owner = owner;
        this.duration = duration;
        this.movable = move;
        this.interruptible = true;
        this.priority = ActionPriority.Override;
        this.speedBuff = speedBuff;
    }

    public override void Enter()
    {
        base.Enter();
        currentDuration = duration;
    }

    public override bool Tick()
    {
        base.Tick();
        currentDuration -= Time.deltaTime;
        if (currentDuration <= 0f)
            return true;
        return false;
    }
}
