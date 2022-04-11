using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Single, Constant }
public class Damage
{
    public float hp = 0f;
    public Vector3 vector = Vector3.zero;
    public float pushForce = 0f;
    public DamageType damageType = DamageType.Single;
}
