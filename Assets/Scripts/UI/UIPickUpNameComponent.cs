using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickUpNameComponent : MonoBehaviour
{
    public Text tooltip;
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
        //GameController.instance.playerController.inventory.onSwitchItem += Show;
        if (!playerTwo)
            GameController.instance.OnInventorySwitchEvent += Show;
        else
            GameController.instance.OnInventorySwitchEventCoop += Show;
    }

    public void Show()
    {
        var cItem = getTargetPlayer().inventory.GetItemInSlot(getTargetPlayer().inventory.currentSlot);
        CancelInvoke();
        if (cItem != null)
        {
            tooltip.text = getTargetPlayer().inventory.GetItemInSlot(getTargetPlayer().inventory.currentSlot).itemName;
            tooltip.color = Color.white;
            Invoke("Hide", 2f);
        }
        else
        {
            Hide();
        }
    }

    void Hide()
    {
        tooltip.color = Color.clear;
    }
}
