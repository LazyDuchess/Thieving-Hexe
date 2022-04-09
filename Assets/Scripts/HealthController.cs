using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    //Debug
    public bool god = false;
    public float hp = 100f;
    public float maxHP = 100f;
    public delegate void DamageDelegate(Damage damage);
    public DamageDelegate damageEvent;
    public DamageDelegate deathEvent;
    protected Rigidbody rigidBody;
    // Start is called before the first frame update

    //Affiliation
    public int team = -1;

    //Return an entity's affiliation id
    public virtual int GetTeam()
    {
        return team;
    }

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public virtual bool IsAlive()
    {
        if (hp > 0f)
            return true;
        return false;
    }

    public Rigidbody GetRigidBody()
    {
        return rigidBody;
    }

    public virtual void GiveHealth(float hp)
    {
        this.hp += hp;
        if (this.hp >= maxHP)
            this.hp = maxHP;
    }

    public virtual void TakeDamage(Damage damage)
    {
        var wasAlive = IsAlive();
        if (!god)
            hp -= damage.hp;
        if (damageEvent != null)
            damageEvent.Invoke(damage);
        if (rigidBody)
        {
            var pushy = (transform.position - damage.vector).normalized * damage.pushForce;
            rigidBody.AddForce(pushy, ForceMode.Impulse);
            //rigidBody.velocity += damage.vector;
        }
        if (hp <= 0f && wasAlive)
        {
            OnDeath(damage);
        }
        return;
    }

    public virtual void OnDeath(Damage killingDamage)
    {
        if (deathEvent != null)
            deathEvent.Invoke(killingDamage);
        return;
    }
}
