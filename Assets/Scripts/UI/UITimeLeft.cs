using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeLeft : MonoBehaviour
{
    public Text timerText;

    private void Awake()
    {
        timerText.color = Color.clear;
    }
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

    // Update is called once per frame
    void Update()
    {
        if (DungeonController.instance.dungeonState.wayOut)
        {
            var timeLeft = DungeonController.instance.GetTimeLeft();
            timerText.color = Color.white;
            timerText.text = ((int)toMinutes(timeLeft)).ToString("D2") + ":" + ((int)toSeconds(timeLeft)).ToString("D2");
        }
        else
            timerText.color = Color.clear;
    }
}
