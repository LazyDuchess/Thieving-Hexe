using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionPriority { Base, Override, Important };
public abstract class Action
{
    public ActionPriority priority = ActionPriority.Base;
    public bool interruptible = true;
    public bool movable = true;
    public float speedBuff = 1f;
    public CharacterComponent owner;
    public Action(CharacterComponent owner)
    {
        this.owner = owner;
    }

    public virtual void Cancel()
    {

    }

    public virtual void Enter()
    {

    }
    public abstract bool Tick();
}
