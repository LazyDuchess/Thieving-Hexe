using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_General : MonoBehaviour
{
    public GameObject player;

    public uint lastGameplayState;


    public void Start()
    {
        GameEventsController.enterIndoorAreaEvent += EnterCastle;
        GameEventsController.enterOutdoorAreaEvent += EnterForest;
        GameEventsController.gameOverEvent += PlayerDefeated;
        GameEventsController.collectFragmentEvent += CollectFragment;
        GameEventsController.pauseEvent += PauseGame;
        GameEventsController.unpauseEvent += UnpauseGame;
        GameEventsController.levelPassEvent += Victory;
        GameEventsController.gameOverEvent += Defeated;
        
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

    void CollectFragment()
    {
        AkSoundEngine.PostEvent("Play_Crystal__Pickup", gameObject);
    }

    void PauseGame()
    {

        AkSoundEngine.GetState("Gameplay", out lastGameplayState);
        AkSoundEngine.SetState("Gameplay", "Menu");
    }

    void UnpauseGame()
    {
        uint gameplayID = 89505537U;
        AkSoundEngine.SetState(gameplayID, lastGameplayState);
    }

    void Victory()
    {
        AkSoundEngine.SetState("Gameplay", "Victory");
    }

    void Defeated()
    {
        AkSoundEngine.SetState("Gameplay", "Defeat");
    }
}
