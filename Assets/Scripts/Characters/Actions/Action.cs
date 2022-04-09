using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionPriority { Base, Override, Important };

class ActionEvent
{
    public float time;
    public int id;
}
public abstract class Action
{
    public ActionPriority priority = ActionPriority.Base;
    public bool interruptible = true;
    public bool movable = true;
    public float speedBuff = 1f;
    public CharacterComponent owner;
    public bool useInventory = true;
    public bool lookAt = true;
    public bool canDash = false;

    float timeSpent = 0f;
    List<ActionEvent> events = new List<ActionEvent>();

    public Action(CharacterComponent owner)
    {
        this.owner = owner;
    }

    public virtual void Cancel()
    {
        owner.SendEvent("Cancel");
    }

    public virtual void Enter()
    {

    }

    public virtual void Event(int id) { }

    protected void QueueEvent(float time, int id)
    {
        var act = new ActionEvent();
        act.time = timeSpent + time;
        act.id = id;
        events.Add(act);
    }

    public virtual bool Tick()
    {
        timeSpent += Time.deltaTime;
        foreach(var element in events)
        {
            if (element.time != -1f && timeSpent >= element.time)
            {
                element.time = -1;
                Event(element.id);
            }
        }
        return false;
    }
}
