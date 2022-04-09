using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintPotionItem : ItemComponent
{
    public float speedMult = 2f;
    public float speedBuff = 0.5f;
    public float duration = 2f;
    public float speedDuration = 2f;
    public float speedDegradeMultiply = 0.2f;
    public override void Primary()
    {
        base.Primary();
        if (!owner.ActionBusy())
        {
            if (!owner.hasEffectWithID("sprint_potion"))
            {
                var drinkAction = new SprintDrinkAction(owner, true, speedBuff, duration, speedMult, speedDuration, speedDegradeMultiply);
                owner.QueueAction(drinkAction);
            }
        }
    }
}
