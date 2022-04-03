using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public AICharacterComponent owner;
    public int priority;
    public Task(AICharacterComponent owner, int priority)
    {
        this.owner = owner;
        this.priority = priority;
    }
    public abstract bool Tick();
}
