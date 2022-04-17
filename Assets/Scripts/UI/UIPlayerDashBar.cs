using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDashBar : MonoBehaviour
{
    private UIBar bar;
    private float lastDash;
    public bool playerTwo = false;

    PlayerController getTargetPlayer()
    {
        if (playerTwo)
            return GameController.instance.coopPlayer;
        return GameController.instance.playerController;
    }
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<UIBar>();
        bar.maxValue = getTargetPlayer().dashTime;
        bar.minValue = 0f;
        bar.SetValue(getTargetPlayer().GetDashStamina());
        lastDash = getTargetPlayer().GetDashStamina();
    }

    private void Update()
    {
        if (getTargetPlayer().GetDashStamina() != lastDash)
        {
            bar.SetValue(getTargetPlayer().GetDashStamina());
            lastDash = getTargetPlayer().GetDashStamina();
        }
    }
}
