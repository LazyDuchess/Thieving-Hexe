using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Enemy : MonoBehaviour
{
   

    [SerializeField] public AK.Wwise.Event eFootsteps;
    [SerializeField] public AK.Wwise.Event eLteAtk;
    [SerializeField] public AK.Wwise.Event eHvyAtk;
    [SerializeField] public AK.Wwise.Event eRcvDmg;
    [SerializeField] public AK.Wwise.Event eDefeated;


    public void PlayEnemyFootstep()//use on animation
    {
        eFootsteps.Post(gameObject);
    }

    public void PlayEnemyLightAttack()
    {
        eLteAtk.Post(gameObject);
    }

    public void PlayEnemyHeavyAttack()
    {
        eHvyAtk.Post(gameObject);
    }

    public void PlayEnemyRecvDamage()
    {
        eRcvDmg.Post(gameObject);
    }

public void PlayEnemyDefeated()
    {
        eDefeated.Post(gameObject);
    }
}
