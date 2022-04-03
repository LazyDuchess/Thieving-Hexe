using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEntry
{
    public HealthController entity;
}
public static class HitUtils
{
    public static List<HitEntry> Hit(int team, Vector3 position, float radius)
    {
        var hits = Physics.OverlapSphere(position, radius);
        if (GameController.instance.hitBoxDebug)
        {
            var hBox = GameObject.Instantiate(GameController.instance.hitBoxDebugPrefab, position, Quaternion.identity);
            hBox.transform.localScale = new Vector3(radius, radius, radius);
        }
        var hitList = new List<HitEntry>();
        foreach(var element in hits)
        {
            var healthComp = element.GetComponent<HealthController>();
            if (healthComp)
            {
                var entry = new HitEntry();
                entry.entity = healthComp;
                if (team == -1 || healthComp.GetTeam() == -1)
                {
                    hitList.Add(entry);
                }
                else
                {
                    if (team != healthComp.GetTeam())
                        hitList.Add(entry);
                }
            }
        }
        return hitList;
    }
}
