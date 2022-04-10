using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsController
{
    public delegate void GameEvent();
  

    //When you lose by dying or running out of time.
    public static GameEvent gameOverEvent;

    //When you beat the level by escaping with the fragment.
    public static GameEvent levelPassEvent;

    //When the player gets damaged.
    public static GameEvent playerDamageEvent;

    //When you reach the end of the dungeon and collect the fragment and have to make a run for it.
    public static GameEvent collectFragmentEvent;

    //Player walks into an outdoor area of the dungeon.
    public static GameEvent enterOutdoorAreaEvent;

    //Player walks into an indoor area of the dungeon.
    public static GameEvent enterIndoorAreaEvent;

    //Player attacks.
    public static GameEvent playerAttackEvent;

    //Player starts a charged attack.
    public static GameEvent playerChargedAttackStartEvent;

    //Player throws a charged attack.
    public static GameEvent playerChargedAttackEndEvent;

    //Player opens a chest.
    public static GameEvent openChestEvent;

    public static void GameOver()
    {
        if (gameOverEvent != null)
            gameOverEvent.Invoke();
    }

    public static void LevelPass()
    {
        if (levelPassEvent != null)
            levelPassEvent.Invoke();
    }

    public static void PlayerDamage(Damage dmg)
    {
        if (playerDamageEvent != null)
            playerDamageEvent.Invoke();
    }

    public static void CollectFragment()
    {
        if (collectFragmentEvent != null)
            collectFragmentEvent.Invoke();
    }

    public static void EnterOutdoorArea()
    {
        if (enterOutdoorAreaEvent != null)
            enterOutdoorAreaEvent.Invoke();
    }

    public static void EnterIndoorArea()
    {
        if (enterIndoorAreaEvent != null)
            enterIndoorAreaEvent.Invoke();
    }

    public static void PlayerAttack()
    {
        if (playerAttackEvent != null)
            playerAttackEvent.Invoke();
    }

    public static void PlayerChargedAttackStart()
    {
        if (playerChargedAttackStartEvent != null)
            playerChargedAttackStartEvent.Invoke();
    }

    public static void PlayerChargedAttackEnd()
    {
        if (playerChargedAttackEndEvent != null)
            playerChargedAttackEndEvent.Invoke();
    }

    public static void OpenChest()
    {
        if (openChestEvent != null)
            openChestEvent.Invoke();
    }

    public static float getPlayerSpeed()
    {
        return GameController.instance.playerController.FlatVelocity().magnitude;
    }
}
