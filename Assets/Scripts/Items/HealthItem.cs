using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : ItemComponent
{
    public float hp = 10f;
    public float speedBuff = 0.5f;
    public float duration = 2f;
    public override void Primary()
    {
        base.Primary();
        if (!owner.ActionBusy())
        {
            if (owner.hp < owner.maxHP)
            {
                var drinkAction = new DrinkAction(owner, true, speedBuff, duration, hp);
                owner.QueueAction(drinkAction);
            }
        }
    }
}
