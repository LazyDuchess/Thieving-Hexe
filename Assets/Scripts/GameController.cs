using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject player;
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
