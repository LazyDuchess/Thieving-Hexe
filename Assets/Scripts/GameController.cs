using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool aiEnabled = true;

    public bool debugSpawnMonsters = false;
    public GameObject monsterPrefab;

    public static GameController instance;
    public GameObject hitBoxDebugPrefab;
    public bool hitBoxDebug = false;
    public GameObject player;
    [HideInInspector]
    public PlayerController playerController;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        playerController = player.GetComponent<PlayerController>();
    }

    public static LayerMask GetGroundMask()
    {
        return LayerMask.GetMask("Ground");
    }
}
