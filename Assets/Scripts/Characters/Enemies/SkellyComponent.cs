using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyComponent : AICharacterComponent
{
    public GameObject gibsPrefab;
    private Action meleeAtkAction;
    protected override void Start()
    {
        base.Start();
        deathEvent += Gib;
    }

    protected override void InitializeTasks()
    {
        base.InitializeTasks();
        meleeAtkAction = new MeleeAttackAction(this, 0.7f, 1.0f, 0.6f, 8f, false);
        AddTask(new MeleeAttackTask(this, 100, 1.75f, 50f, meleeAtkAction, 0.35f));
    }

    void Gib(Damage damage)
    {
        var gibsObject = Instantiate(gibsPrefab, transform.position, mesh.transform.rotation);
        Destroy(gameObject);
    }
}
