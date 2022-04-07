using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : RoomTrigger
{
    public GameObject prefab;
    public bool spawnOnArrival = true;
    public bool spawnOnWayOut = false;

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
    }

    public override void OnPlayerEnter()
    {
        if (spawnOnArrival)
        {
            spawn();
        }
    }
}