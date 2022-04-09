using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintDrinkAction : Action
{
    bool done = false;
    float hp = 10f;
    float duration = 2f;
    float speedMult = 1f;
    float sprintDuration;
    float sprintDegradeMultiply;
    public SprintDrinkAction(CharacterComponent owner, bool move, float speedBuff, float duration, float speedMult, float sprintDuration, float sprintDegradeMultiply) : base(owner)
    {
        this.owner = owner;
        this.movable = move;
        this.interruptible = true;
        this.priority = ActionPriority.Base;
        this.speedBuff = speedBuff;
        this.useInventory = false;
        this.lookAt = false;
        this.duration = duration;
        this.speedMult = speedMult;
        this.sprintDuration = sprintDuration;
        this.sprintDegradeMultiply = sprintDegradeMultiply;
    }

    public override void Enter()
    {
        base.Enter();
        owner.SendEvent("Drink");
        QueueEvent(duration, 0);
    }

    public override void Event(int id)
    {
        switch(id)
        {
            case 0:
                var eff = new CharacterEffect();
                eff.id = "sprint_potion";
                eff.duration = sprintDuration;
                eff.sprintSpeedMultiplier = speedMult;
                eff.sprintDegradeMultiplier = sprintDegradeMultiply;
            done = true;
                owner.AddEffect(eff);
                owner.ResetDash();
                var playa = owner.GetComponent<PlayerController>();
                if (playa)
                    playa.UseCurrentInventory();
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
