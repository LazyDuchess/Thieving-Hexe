using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : RoomTrigger
{
    public GameObject prefab;
    public bool spawnOnArrival = true;
    public bool spawnOnWayOut = false;
    public bool bindToLevel = true;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void spawn()
    {
        var pref = Instantiate(prefab, transform.position, transform.rotation);
        if (!bindToLevel)
            pref.transform.SetParent(transform.parent);
        else
            pref.transform.SetParent(DungeonController.instance.level.transform);
    }

    public override void OnPlayerEnter()
    {
        if (spawnOnArrival)
        {
            spawn();
        }
    }

    public override void OnPlayerComeBack()
    {
        if (spawnOnWayOut)
        {
            spawn();
        }
    }
}
