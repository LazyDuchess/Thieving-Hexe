using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkAction : Action
{
    bool done = false;
    float hp = 10f;
    float duration = 2f;
    public DrinkAction(CharacterComponent owner, bool move, float speedBuff, float duration, float hp) : base(owner)
    {
        this.owner = owner;
        this.movable = move;
        this.interruptible = true;
        this.priority = ActionPriority.Base;
        this.speedBuff = speedBuff;
        this.useInventory = false;
        this.lookAt = false;
        this.duration = duration;
        this.hp = hp;
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
            owner.GiveHealth(hp);
            done = true;
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
