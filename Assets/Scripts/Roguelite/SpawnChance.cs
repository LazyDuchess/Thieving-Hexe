using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChance : MonoBehaviour
{
    public int spawnChance = 4;
    private void Start()
    {
        var chanc = Random.Range(0, spawnChance);
        if (chanc != 0)
            gameObject.SetActive(false);
    }
}
