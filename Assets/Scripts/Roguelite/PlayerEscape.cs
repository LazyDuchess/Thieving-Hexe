using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscape : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var otherPlayer = other.GetComponent<PlayerController>();
        if(otherPlayer && otherPlayer.IsAlive() && DungeonController.instance.dungeonState.wayOut && !DungeonController.instance.dungeonState.done)
        {
            GameController.instance.CompleteDungeon();
        }
    }
}
