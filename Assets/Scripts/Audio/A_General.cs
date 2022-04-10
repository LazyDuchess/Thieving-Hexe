using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_General : MonoBehaviour
{
    public GameObject player;


    public void Start()
    {
        GameEventsController.enterIndoorAreaEvent += EnterCastle;
        GameEventsController.enterOutdoorAreaEvent += EnterForest;
        GameEventsController.gameOverEvent += PlayerDefeated;
        
    }
    public void Update()
    {
        var speed = GameEventsController.getPlayerSpeed();
        AkSoundEngine.SetRTPCValue("PlayerSpeed", speed, player);


    }


    void PlayerDefeated()
    {
        AkSoundEngine.SetState("Gameplay", "Defeated");

    }
    void EnterForest()
    {
        AkSoundEngine.SetState("Enviro", "Forset");
    }
    void EnterCastle()
    {
        AkSoundEngine.SetState("Enviro", "Castle");
    }
}
