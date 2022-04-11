using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelCompleteStats : MonoBehaviour
{
    public Text stats;
    float toMinutes(float seconds)
    {
        var mins = Mathf.Floor(seconds / 60f);
        return mins;
    }

    float toSeconds(float seconds)
    {
        var secs = Mathf.Floor(seconds);
        secs -= Mathf.Floor(secs / 60f) * 60f;
        return secs;
    }
    public void Show()
    {
        var timeLeft = DungeonController.instance.GetTimeLeft();
        var totalTime = DungeonController.instance.GetTimeElapsed();
        var totalTimerText = ((int)toMinutes(totalTime)).ToString("D2") + ":" + ((int)toSeconds(totalTime)).ToString("D2");
        var timerText = ((int)toMinutes(timeLeft)).ToString("D2") + ":" + ((int)toSeconds(timeLeft)).ToString("D2");
        stats.text = "Dungeon "+(GameController.instance.gameGlobals.currentDungeon+1)+"\nKilled "+DungeonController.instance.dungeonState.killedEnemies+" Enemies out of "+DungeonController.instance.dungeonState.spawnedEnemies+"\nEscaped with "+timerText+" left\nTotal level completion time: "+totalTimerText;
    }
}
