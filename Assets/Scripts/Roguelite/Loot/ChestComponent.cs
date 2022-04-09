using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestComponent : InteractableComponent
{
    public GameObject topClosed;
    public GameObject topOpen;
    public List<GameObject> lootTable;
    public List<GameObject> rareLootTable;
    public int rareLootChance = 0;
    public bool opened = false;
    public int amountMin = 1;
    public int amountMax = 3;
    public float velocity;
    public float offset = 1f;
    public float rad = 2f;
    public float forwardVel = -8f;

    public override bool Interactable()
    {
        if (!opened)
            return true;
        return false;
    }

    public void MakeLoot()
    {
        var lootAm = Random.Range(amountMin, amountMax);
        for (var i = 0; i < lootAm; i++)
            MakeLootItem();
    }

    public void MakeLootItem()
    {
        var loot = lootTable;
        if (rareLootChance > 0)
        {
            var chance = Random.Range(0, rareLootChance);
            if (chance == 0)
                loot = rareLootTable;
        }
        var obj = Instantiate(lootTable[Random.Range(0, lootTable.Count)], transform.position + (Vector3.up*offset),Quaternion.identity);
        var rdB = obj.GetComponentInChildren<Rigidbody>();
        rdB.AddExplosionForce(velocity, transform.position, rad);
        rdB.AddForce(transform.forward * forwardVel, ForceMode.Impulse);
    }

    public override void Interact(CharacterComponent actor)
    {
        GameEventsController.OpenChest();
        topClosed.SetActive(false);
        topOpen.SetActive(true);
        MakeLoot();
        opened = true;
    }
}
