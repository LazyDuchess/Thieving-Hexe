using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxLife = 10f;
    public float damage = 10f;
    public CharacterComponent owner;
    public Vector3 vector;
    bool dormant = false;
    Light lite;
    public float pushForce = 3f;
    public delegate void OnHit();
    public OnHit onHitEvent;

    private void Start()
    {
        Invoke("Delete", maxLife);
        GetComponent<Rigidbody>().velocity = vector;
        lite = GetComponentInChildren<Light>();
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (dormant)
        {
            lite.intensity = Mathf.Lerp(lite.intensity, 0f, 1f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (dormant)
            return;
        var otherHP = other.collider.GetComponent<HealthController>();
        var dontDel = false;
        if (otherHP)
        {
            if ((otherHP.GetTeam() == -1 || owner.GetTeam() == -1) || (otherHP.GetTeam() != owner.GetTeam()))
            {
                var dam = new Damage();
                dam.hp = 10f;
                dam.vector = transform.position;
                dam.pushForce = pushForce;
                otherHP.TakeDamage(dam);
            }
            else
                dontDel = true;
        }
        if (!dontDel)
        {
            CancelInvoke();
            dormant = true;
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<SphereCollider>());
            GetComponentInChildren<ParticleSystem>().Stop();
            Invoke("Delete", 10f);
            if (onHitEvent != null)
                onHitEvent.Invoke();
            //Destroy(gameObject);
        }
    }
}
