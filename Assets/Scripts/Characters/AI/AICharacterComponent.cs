using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterComponent : CharacterComponent
{
    private List<Task> tasks = new List<Task>();

    public List<Task> Tasks
    {
        get
        {
            return new List<Task>(tasks);
        }
    }

    protected override void Start()
    {
        base.Start();
        InitializeTasks();
    }
    
    protected override void Update()
    {
        base.Update();
        movementVector = Vector3.zero;
        if (IsAlive() && GameController.instance.aiEnabled)
        {
            foreach(var element in tasks)
            {
                var returnValue = element.Tick();
                if (returnValue)
                    break;
            }
        }
    }

    public void AddTask(Task task)
    {
        var insertIndex = 0;
        for(var i=0;i<tasks.Count;i++)
        {
            insertIndex = i;
            if (tasks[i].priority < task.priority)
                break;
        }
        tasks.Insert(insertIndex, task);
    }

    protected virtual void InitializeTasks()
    {
    }
}
