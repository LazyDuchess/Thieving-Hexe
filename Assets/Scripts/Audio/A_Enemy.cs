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

    public float footStepRepeat = 0.25f;
    public float footStepSpeedThreshold = 5f;
    float currentFootStepTimer = 0.25f;
    public CharacterComponent me;
    bool wasRunning = false;

    private void Start()
    {
        me.deathEvent += PlayEnemyDefeated;
    }

    private void Update()
    {
        var mySpeed = me.FlatVelocity().magnitude;
        if (mySpeed >= footStepSpeedThreshold)
        {
            if (!wasRunning)
            {
                currentFootStepTimer = footStepRepeat;
            }
            wasRunning = true;
            currentFootStepTimer += Time.deltaTime;
            if (currentFootStepTimer >= footStepRepeat)
            {
                eFootsteps.Stop(gameObject);
                eFootsteps.Post(gameObject);
                currentFootStepTimer = 0f;
            }
        }
        else
        {
            wasRunning = false;
            currentFootStepTimer = 0f;
            eFootsteps.Stop(gameObject);
        }
    }


    public void PlayEnemyFootstep()//use on animation
    {
        //eFootsteps.Post(gameObject);
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

public void PlayEnemyDefeated(Damage dmg)
    {
        eDefeated.Post(gameObject);
        eFootsteps.Stop(gameObject);
    }
}
