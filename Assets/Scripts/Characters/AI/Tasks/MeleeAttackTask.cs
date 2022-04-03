using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTask : Task
{
    private CharacterComponent target = null;
    private float minDistance = 1f;
    private Action meleeAttackAction;
    private float targetDistance = 20f;
    public MeleeAttackTask(AICharacterComponent owner, int priority, float attackDistance, float targetDistance, Action meleeAttackAction) : base(owner, priority)
    {
        this.minDistance = attackDistance;
        this.meleeAttackAction = meleeAttackAction;
        this.targetDistance = targetDistance;
    }

    void getTarget()
    {
        var chars = GameObject.FindObjectsOfType<CharacterComponent>();
        CharacterComponent lastChar = null;
        float lastDistance = 0f;
        foreach(var element in chars)
        {
            var distance = Vector3.Distance(owner.transform.position, element.transform.position);
            if (element.IsAlive() && element.GetTeam() != owner.GetTeam() && distance <= targetDistance)
            {
                
                if (lastChar == null)
                {
                    lastChar = element;
                    lastDistance = distance;
                }
                else
                {
                    if (distance < lastDistance)
                    {
                        lastChar = element;
                        lastDistance = distance;
                    }
                }
            }
        }
        if (lastChar == null)
        {
            if (target && target.IsAlive() && target.GetTeam() != owner.GetTeam())
                lastChar = target;
        }
        target = lastChar;
    }

    public override bool Tick()
    {
        getTarget();
        if (target != null)
        {
            var dist = Vector3.Distance(owner.transform.position, target.transform.position);
            var heading = (owner.transform.position - target.transform.position).normalized;
            if (dist > minDistance)
            {
                owner.movementVector = heading;
            }
            else
            {
                if (!owner.ActionBusy())
                {
                    owner.QueueAction(meleeAttackAction);
                }
                owner.movementVector = Vector3.zero;
                owner.SetRotation(Quaternion.LookRotation(-heading).eulerAngles.y);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
