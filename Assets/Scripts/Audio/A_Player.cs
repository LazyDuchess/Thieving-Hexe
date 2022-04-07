using System.Collections;
using System.Collections.Generic;
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

//Things to be accessed by anyone
#region Public Variables
public PlayerController pController;//get player public class
    #endregion

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pFootStep;
    //[SerializeField] private AK.Wwise.Event pJump;
    //[SerializeField] private AK.Wwise.Event pLand;
    [Header("Attacks")]
    [SerializeField] private AK.Wwise.Event pBasicAttack;
    [SerializeField] private AK.Wwise.Event pFireAtkkLte;
    [SerializeField] private AK.Wwise.Event pFireAtkChrg;
    [SerializeField] private AK.Wwise.Event pWaterAtkLte;
    [SerializeField] private AK.Wwise.Event pWaterAtkChrg;
    [SerializeField] private AK.Wwise.Event pElecAtkLte;
    [SerializeField] private AK.Wwise.Event pElecAtkChrg;
    [Header("Receive Damage")]
    [SerializeField] private AK.Wwise.Event pTakeDmgGeneric;
    [SerializeField] private AK.Wwise.Event pTakeDmgFire;
    [SerializeField] private AK.Wwise.Event pTakeDmgElec;
    [SerializeField] private AK.Wwise.Event pDefeated;
    #endregion

    public void Update()
    {
        //get and set player health RTPC
        AkSoundEngine.SetRTPCValue("PlayerHealth", pController.hp); 
        
    }

    ///////////////Begin Animation Functions////////////////

    public void PlayPlayerFootstep()
    {
        pFootStep.Post(gameObject);
    }

    public void PlayPlayerDeath()
    {
        pDefeated.Post(gameObject);
    }


    //////////END animation Functions////////////







}//END MAIN