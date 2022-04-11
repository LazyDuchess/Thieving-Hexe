using UnityEngine;

/*
Sounds that the player makes

Sounds:
-Footsteps
-Jump
-Land
-Basic Attack
-Fire/Water/Electrical Attacks
---Small
---Charge
-Take Damage
--Generic
--Fire
--Electrical
RTPCs:
--Health
States:
-Surface type
*/
public class A_Player : MonoBehaviour
{


    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pFootStep;
    [SerializeField] private AK.Wwise.Event pStopFootStep;

    [Header("Attacks")]
    [SerializeField] private AK.Wwise.Event pLteAttack;
    [SerializeField] private AK.Wwise.Event pChrgAttackStartLoop;
    [SerializeField] private AK.Wwise.Event pChrgAttackRelease;
    /*    [SerializeField] private AK.Wwise.Event pFireAtkkLte;
        [SerializeField] private AK.Wwise.Event pFireAtkChrg;
        [SerializeField] private AK.Wwise.Event pWaterAtkLte;
        [SerializeField] private AK.Wwise.Event pWaterAtkChrg;
        [SerializeField] private AK.Wwise.Event pElecAtkLte;
        [SerializeField] private AK.Wwise.Event pElecAtkChrg;*/
    [Header("Receive Damage")]
    [SerializeField] private AK.Wwise.Event pTakeDmgGeneric;
    [SerializeField] private AK.Wwise.Event pTakeDmgFire;
    // [SerializeField] private AK.Wwise.Event pTakeDmgElec;
    [SerializeField] private AK.Wwise.Event pDefeated;
    [SerializeField] private AK.Wwise.Event pTorchWave;

    [Header("Items")]
    [SerializeField] private AK.Wwise.Event pKeyPu;
    [SerializeField] private AK.Wwise.Event pPotionPu;
    [SerializeField] private AK.Wwise.Event pDrinkPotion;



    #endregion

    public void Start()
    {
 
      
        GameEventsController.playerChargedAttackStartEvent += PlayChargeAttackStart;
        GameEventsController.playerChargedAttackEndEvent += PlayChargeAttackCast;
        GameEventsController.playerAttackEvent += PlayBasicAttack;
        GameEventsController.playerDamageEvent += PlayTakeDamage;
        GameEventsController.gameOverEvent += PlayPlayerDeath;
        GameEventsController.playerMeleeAttackEvent += PlayTorchWave;
        GameEventsController.pickUpKeyEvent += PlayKeyPu;

        //Mistake?
        //GameEventsController.pickUpOtherEvent += PlayPotionPu;

        GameEventsController.pickUpPotionEvent += PlayPotionPu;
        GameEventsController.potionDrinkEvent += PlayDrinkPotion;

        pFootStep.Post(gameObject); //start the float sound and use the RTPC to choose the volume of it

        GameEventsController.preRestartEvent += Clean;
    }

    void Clean()
    {
        GameEventsController.playerChargedAttackStartEvent -= PlayChargeAttackStart;
        GameEventsController.playerChargedAttackEndEvent -= PlayChargeAttackCast;
        GameEventsController.playerAttackEvent -= PlayBasicAttack;
        GameEventsController.playerDamageEvent -= PlayTakeDamage;
        GameEventsController.gameOverEvent -= PlayPlayerDeath;
        GameEventsController.playerMeleeAttackEvent -= PlayTorchWave;
        GameEventsController.pickUpKeyEvent -= PlayKeyPu;
        GameEventsController.pickUpPotionEvent -= PlayPotionPu;
        GameEventsController.potionDrinkEvent -= PlayDrinkPotion;
        GameEventsController.preRestartEvent -= Clean;
    }

    public void Update()
    {

            AkSoundEngine.SetRTPCValue("PlayerHealth", GameController.instance.playerController.hp);

        AkSoundEngine.SetRTPCValue("PlayerSpeed", GameEventsController.getPlayerSpeed()/2f);
    }


/*    void PlayPotionPu()
    {
        if(pController.Interact)   
    }
*/

    void PlayBasicAttack()
    {
        pLteAttack.Post(gameObject);
    }


    void PlayChargeAttackStart()
    {
        pChrgAttackStartLoop.Post(gameObject);
    }

    void PlayChargeAttackCast()
    {
        pChrgAttackRelease.Post(gameObject);
    }

    void PlayPlayerDeath()
    {
        pDefeated.Post(gameObject);
    }

    void PlayTakeDamage(Damage dmg)
    {
        if (GameController.instance.playerController.IsAlive())
        {
            if (dmg.damageType == DamageType.Constant)//take fire damage
                pTakeDmgFire.Post(gameObject);
            else
                pTakeDmgGeneric.Post(gameObject);
        }
    }

    void PlayTorchWave()
    {
        pTorchWave.Post(gameObject);
    }
  

    void PlayKeyPu()
    {
        pKeyPu.Post(gameObject);
    }

    void PlayPotionPu()
    {
        pPotionPu.Post(gameObject);
    }

    void PlayDrinkPotion()
    {
        pDrinkPotion.Post(gameObject);
    }


}//END MAIN