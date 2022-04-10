using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool controlEnabled = true;

    public GameGlobals gameGlobals = new GameGlobals();
    public int playerTeam = 0;

    public bool aiEnabled = true;

    public bool debugSpawnMonsters = false;
    public GameObject monsterPrefab;

    public static GameController instance;
    public GameObject hitBoxDebugPrefab;
    public bool hitBoxDebug = false;
    public GameObject player;
    [HideInInspector]
    public PlayerController playerController;

    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;
    public GameObject gameplayScreen;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }
        //DontDestroyOnLoad(this.gameObject);
        instance = this;
        playerController = player.GetComponent<PlayerController>();
        playerController.deathEvent += PlayerDeathEv;
        playerController.damageEvent += GameEventsController.PlayerDamage;
    }

    public void PlayerDeathEv(Damage dmg)
    {
        GameOver();
    }

    public static LayerMask GetGroundMask()
    {
        return LayerMask.GetMask("Ground");
    }
}
