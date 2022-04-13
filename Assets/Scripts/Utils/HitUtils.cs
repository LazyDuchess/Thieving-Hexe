using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEntry
{
    public HealthController entity;
}
public static class HitUtils
{
    public static List<HitEntry> Hit(CharacterComponent owner, Vector3 position, float radius)
    {
        var team = owner.GetTeam();
        
        var hits = Physics.OverlapSphere(position, radius);
        if (GameController.instance.hitBoxDebug)
        {
            var hBox = GameObject.Instantiate(GameController.instance.hitBoxDebugPrefab, position, Quaternion.identity);
            hBox.transform.localScale = new Vector3(radius, radius, radius);
        }
        var hitList = new List<HitEntry>();
        foreach(var element in hits)
        {
            var checkRay = VectorUtil.MakeRay(owner.transform.position, element.transform.position, 0.1f);
            var ray = new Ray(checkRay.origin, checkRay.heading);
            var testRay = Physics.RaycastAll(ray, checkRay.distance);
            var notValid = true;
            foreach (var elementRay in testRay)
            {
                notValid = false;
                var allTest = new List<HealthController>(elementRay.collider.GetComponents<HealthController>());
                allTest.AddRange(elementRay.collider.GetComponentsInChildren<HealthController>());
                allTest.AddRange(elementRay.collider.GetComponentsInParent<HealthController>());
                if (allTest.Count > 0)
                    notValid = true;
                if (elementRay.collider.CompareTag("NonBlocking"))
                    notValid = true;
            }
            if (notValid)
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
        }
        return hitList;
    }
}
