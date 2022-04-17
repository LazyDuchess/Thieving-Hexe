using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Coop")]
    public bool coopMode = false;

    public GameObject coopPlayerPrefab;

    public PlayerController coopPlayer;

    public GameObject playerPrefab;

    public bool firstPerson = false;

    public bool audioHacks = true;

    public bool controlEnabled = true;

    public GameGlobals gameGlobals = new GameGlobals();
    public int playerTeam = 0;

    public bool aiEnabled = true;

    public bool debugSpawnMonsters = false;
    public GameObject monsterPrefab;

    public static GameController instance;
    public GameObject hitBoxDebugPrefab;
    public bool hitBoxDebug = false;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerController playerController;

    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;
    public GameObject gameplayScreen;

    private static bool cachedCharacters = false;
    private static bool cachedItems = false;
    private static bool cachedInteractables = false;
    private static CharacterComponent[] charactersThisFrame;
    private static ItemComponent[] allItems;
    private static InteractableComponent[] allInteractables;

    private static LayerMask groundMask;

    private int busyAmount = 0;

    public delegate void GameEvent();

    public GameEvent OnInventorySwitchEvent;
    public GameEvent OnInventoryUpdateEvent;

    public GameEvent OnInventorySwitchEventCoop;
    public GameEvent OnInventoryUpdateEventCoop;

    public void OnInvSwitch()
    {
        if (OnInventorySwitchEvent != null)
            OnInventorySwitchEvent.Invoke();
    }

    public void OnInvUpdate()
    {
        if (OnInventoryUpdateEvent != null)
            OnInventoryUpdateEvent.Invoke();
    }

    public void OnInvSwitchCoop()
    {
        if (OnInventorySwitchEventCoop != null)
            OnInventorySwitchEventCoop.Invoke();
    }

    public void OnInvUpdateCoop()
    {
        if (OnInventoryUpdateEventCoop != null)
            OnInventoryUpdateEventCoop.Invoke();
    }

    public static bool GetBusy()
    {
        if (instance.busyAmount > 0)
            return true;
        return false;
    }

    public static void PushBusy()
    {
        instance.busyAmount += 1;
    }

    public static void PopBusy()
    {
        instance.busyAmount -= 1;
    }

    

    public static bool GetAudioHacks()
    {
        return instance.audioHacks;
    }

    private static void cacheInteractables()
    {
        allInteractables = FindObjectsOfType<InteractableComponent>();
        cachedInteractables = true;
    }

    private static void cacheCharacters()
    {
        charactersThisFrame = FindObjectsOfType<CharacterComponent>();
        cachedCharacters = true;
    }

    private static void cacheItems()
    {
        allItems = FindObjectsOfType<ItemComponent>();
        cachedItems = true;
    }

    public static void dirtyCharacters()
    {
        cachedCharacters = false;
    }

    public static void dirtyItems()
    {
        cachedItems = false;
    }

    public static void dirtyInteractables()
    {
        cachedInteractables = false;
    }

    public static ItemComponent[] GetItems()
    {
        if (!cachedItems)
            cacheItems();
        return allItems;
    }

    public static CharacterComponent[] GetCharacters()
    {
        if (!cachedCharacters)
            cacheCharacters();
        return charactersThisFrame;
    }

    public static InteractableComponent[] GetInteractables()
    {
        if (!cachedInteractables)
            cacheInteractables();
        return allInteractables;
    }

    public void CompleteDungeon()
    {
        controlEnabled = false;
        var dungeonController = DungeonController.instance;
        dungeonController.dungeonState.done = true;
        aiEnabled = false;
        gameGlobals.survivedDungeons += 1;
        gameGlobals.spawnedEnemies += dungeonController.dungeonState.spawnedEnemies;
        gameGlobals.killedEnemies += dungeonController.dungeonState.killedEnemies;
        gameGlobals.timeLeft = dungeonController.dungeonState.timeLeft;
        gameplayScreen.SetActive(false);
        levelCompleteScreen.SetActive(true);
        levelCompleteScreen.SendMessage("Show");
        GameEventsController.LevelPass();
    }

    public void GameOver()
    {
        controlEnabled = false;
        var dungeonController = DungeonController.instance;
        dungeonController.dungeonState.done = true;
        gameGlobals.spawnedEnemies += dungeonController.dungeonState.spawnedEnemies;
        gameGlobals.killedEnemies += dungeonController.dungeonState.killedEnemies;
        gameplayScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScreen.SendMessage("Show");
        GameEventsController.GameOver();
    }

    public void NextDungeon()
    {
        aiEnabled = true;
        gameplayScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);
        controlEnabled = true;
        gameGlobals.currentDungeon += 1;
        DungeonController.instance.RegenerateLevel();
        playerController.StripAllEffects();
        playerController.ResetDash();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        PauseController.Unpause();
        GameEventsController.preRestartEvent();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPlayer(PlayerController player)
    {
        if (playerController)
        {
            playerController.deathEvent -= PlayerDeathEv;
            playerController.damageEvent -= GameEventsController.PlayerDamage;
            playerController.inventory.onDropItem -= OnInvUpdate;
            playerController.inventory.onSwitchItem -= OnInvSwitch;
            playerController.inventory.onAddItem -= OnInvUpdate;
        }
        this.player = player.gameObject;
        this.playerController = player;
        playerController.deathEvent += PlayerDeathEv;
        playerController.damageEvent += GameEventsController.PlayerDamage;
        playerController.inventory.onDropItem += OnInvUpdate;
        playerController.inventory.onSwitchItem += OnInvSwitch;
        playerController.inventory.onAddItem += OnInvUpdate;
    }

    public void SetCoopPlayer(PlayerController player)
    {
        if (coopPlayer)
        {
            coopPlayer.deathEvent -= PlayerDeathEv;
            coopPlayer.damageEvent -= GameEventsController.PlayerDamage;
            coopPlayer.inventory.onDropItem -= OnInvUpdateCoop;
            coopPlayer.inventory.onSwitchItem -= OnInvSwitchCoop;
            coopPlayer.inventory.onAddItem -= OnInvUpdateCoop;
        }
        this.coopPlayer = player;
        coopPlayer.deathEvent += PlayerDeathEv;
        coopPlayer.damageEvent += GameEventsController.PlayerDamage;
        coopPlayer.inventory.onDropItem += OnInvUpdateCoop;
        coopPlayer.inventory.onSwitchItem += OnInvSwitchCoop;
        coopPlayer.inventory.onAddItem += OnInvUpdateCoop;
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }
        player = Instantiate(playerPrefab);
        player.name = "Player";
        instance = this;
        SetPlayer(player.GetComponent<PlayerController>());
        groundMask = LayerMask.GetMask("Ground");

        if (coopMode)
        {
            var player2 = Instantiate(coopPlayerPrefab);
            player2.name = "Player2";
            SetCoopPlayer(player2.GetComponent<PlayerController>());
        }
    }

    public void PlayerDeathEv(Damage dmg)
    {
        GameOver();
        GameEventsController.PlayerDeath(dmg);
    }

    public static LayerMask GetGroundMask()
    {
        return groundMask;
    }
}
