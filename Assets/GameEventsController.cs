public class GameEventsController
{
    public delegate void GameEvent();
    public delegate void DamageEvent(Damage damage);


    //When you lose by dying or running out of time.
    public static GameEvent gameOverEvent;

    //When you beat the level by escaping with the fragment.
    public static GameEvent levelPassEvent;

    //When the player gets damaged.
    public static DamageEvent playerDamageEvent;

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

    //Player opens a door with a castle key.
    public static GameEvent openDoorEvent;

    //Dungeon starts.
    public static GameEvent dungeonStartEvent;

    //Rooms open because enemies have been defeated.
    public static GameEvent roomsOpenEvent;

    //Rooms close because enemies have spawned.
    public static GameEvent roomsCloseEvent;

    //Player melee attacks.
    public static GameEvent playerMeleeAttackEvent;

    //Game paused
    public static GameEvent pauseEvent;

    //Game unpaused
    public static GameEvent unpauseEvent;

    //Potion picking up
    public static GameEvent pickUpPotionEvent;

    //Key picking up
    public static GameEvent pickUpKeyEvent;

    //Other picking up
    public static GameEvent pickUpOtherEvent;

    //Drinking potion
    public static GameEvent potionDrinkEvent;

    //Restart
    public static GameEvent preRestartEvent;

    //Player Death
    public static DamageEvent playerDeathEvent;

    public static void PreRestart()
    {
        if (preRestartEvent != null)
            preRestartEvent.Invoke();
    }

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
            playerDamageEvent.Invoke(dmg);
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

    public static void OpenDoor()
    {
        if (openDoorEvent != null)
            openDoorEvent.Invoke();
    }

    public static void DungeonStart()
    {
        if (dungeonStartEvent != null)
            dungeonStartEvent.Invoke();
    }

    public static void RoomsOpen()
    {
        if (roomsOpenEvent != null)
            roomsOpenEvent.Invoke();
    }

    public static void RoomsClose()
    {
        if (roomsCloseEvent != null)
            roomsCloseEvent.Invoke();
    }

    public static void PlayerMeleeAttack()
    {
        if (playerMeleeAttackEvent != null)
            playerMeleeAttackEvent.Invoke();
    }

    public static void PauseGame()
    {
        if (pauseEvent != null)
            pauseEvent.Invoke();
    }

    public static void UnpauseGame()
    {
        if (unpauseEvent != null)
            unpauseEvent.Invoke();
    }

    public static void PickUpPotion()
    {
        if (pickUpPotionEvent != null)
            pickUpPotionEvent.Invoke();
    }

    public static void PickUpKey()
    {
        if (pickUpKeyEvent != null)
            pickUpKeyEvent.Invoke();
    }

    public static void PickUpOther()
    {
        if (pickUpOtherEvent != null)
            pickUpOtherEvent.Invoke();
    }

    public static void DrinkPotion()
    {
        if (potionDrinkEvent != null)
            potionDrinkEvent.Invoke();
    }

    public static void PlayerDeath(Damage dmg)
    {
        if (playerDeathEvent != null)
            playerDeathEvent.Invoke(dmg);
    }

    public static float getPlayerSpeed()
    {
        return GameController.instance.playerController.FlatVelocity().magnitude;
    }
}
