using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerComponent : MonoBehaviour
{
    public float damage = 1f;
    public float interval = 2f;
    public bool constant = true;
    public DamageType damageType = DamageType.Constant;

    private Dictionary<HealthController, float> secondaryDictionary = new Dictionary<HealthController, float>();
    private Dictionary<HealthController,float> affectedEntities = new Dictionary<HealthController,float>();

    void TriggerDamage(Collider other)
    {
        var otherController = other.GetComponent<HealthController>();
        if (otherController)
        {
            if (!affectedEntities.ContainsKey(otherController) || affectedEntities[otherController] <= 0f)
            {
                var dmg = new Damage();
                dmg.hp = damage;
                dmg.damageType = damageType;
                otherController.TakeDamage(dmg);
                if (interval > 0f)
                {
                    affectedEntities[otherController] = interval;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!constant)
            TriggerDamage(other);
    }

    void OnTriggerStay(Collider other)
    {
        if (constant)
            TriggerDamage(other);
    }

    void Update()
    {
        secondaryDictionary.Clear();
        foreach(var element in affectedEntities)
        {
            if (element.Value > 0f)
            {
                secondaryDictionary[element.Key] = element.Value - Time.deltaTime;
            }
        }
        affectedEntities = new Dictionary<HealthController, float>(secondaryDictionary);
    }
}
