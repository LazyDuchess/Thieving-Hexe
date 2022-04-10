using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverStats : MonoBehaviour
{
    public Text stats;
    public void Show()
    {
        stats.text = "Survived "+GameController.instance.gameGlobals.survivedDungeons.ToString()+" Dungeons\nKilled "+GameController.instance.gameGlobals.killedEnemies.ToString()+" Enemies out of "+GameController.instance.gameGlobals.spawnedEnemies.ToString();
    }
}
