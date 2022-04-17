using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropTooltip : MonoBehaviour
{
    public Text tooltip;
    public bool playerTwo = false;

    PlayerController getTargetPlayer()
    {
        if (playerTwo)
            return GameController.instance.coopPlayer;
        return GameController.instance.playerController;
    }
    void Start()
    {
        //GameController.instance.playerController.inventory.onSwitchItem += UpdateTooltip;
        GameController.instance.OnInventorySwitchEvent += UpdateTooltip;
        UpdateTooltip();
    }

    void UpdateTooltip()
    {
        var cItem = getTargetPlayer().inventory.GetItemInSlot(getTargetPlayer().inventory.currentSlot);
        if (cItem == null)
            tooltip.color = Color.clear;
        else
        {
            if (cItem.droppable)
                tooltip.color = Color.white;
            else
                tooltip.color = Color.clear;
        }
    }
}
