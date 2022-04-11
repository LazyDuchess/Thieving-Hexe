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
        GameEventsController.playerDeathEvent += PlayPlayerDeath;
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
        //GameEventsController.gameOverEvent -= PlayPlayerDeath;
        GameEventsController.playerDeathEvent -= PlayPlayerDeath;
        GameEventsController.playerMeleeAttackEvent -= PlayTorchWave;
        GameEventsController.pickUpKeyEvent -= PlayKeyPu;
        GameEventsController.pickUpPotionEvent -= PlayPotionPu;
        GameEventsController.potionDrinkEvent -= PlayDrinkPotion;
        GameEventsController.preRestartEvent -= Clean;
    }

    public void Update()
    {

            AkSoundEngine.SetRTPCValue("PlayerHealth", GameController.instance.playerController.hp);

        AkSoundEngine.SetRTPCValue("PlayerSpeed", GameEventsController.getPlayerSpeed()*0.3f); //ToDo: Do this in wwise?
    }


/*    void PlayPotionPu()
    {
        if(pController.Interact)   
    }
*/

    void PlayBasicAttack()
    {
        if (GameController.GetAudioHacks())
            pLteAttack.Post(GameCamera.instance.gameObject);
        else
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

    void PlayPlayerDeath(Damage dmg)
    {
        pDefeated.Post(gameObject);
    }

    void PlayTakeDamage(Damage dmg)
    {
        if (GameController.instance.playerController.IsAlive())
        {
            var gObject = gameObject;
            if (GameController.GetAudioHacks())
                gObject = GameCamera.instance.gameObject;
            if (dmg.damageType == DamageType.Constant)//take fire damage
                pTakeDmgFire.Post(gObject);
            else
                pTakeDmgGeneric.Post(gObject);
        }
    }

    void PlayTorchWave()
    {
        if (GameController.GetAudioHacks())
            pTorchWave.Post(GameCamera.instance.gameObject);
        else
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
        pDrinkPotion.Stop(gameObject);
        pDrinkPotion.Post(gameObject);
    }


}//END MAIN