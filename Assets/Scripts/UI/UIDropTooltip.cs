using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropTooltip : MonoBehaviour
{
    public Text tooltip;
    // Start is called before the first frame update
    void Start()
    {
        //GameController.instance.playerController.inventory.onSwitchItem += UpdateTooltip;
        GameController.instance.OnInventorySwitchEvent += UpdateTooltip;
        UpdateTooltip();
    }

    void UpdateTooltip()
    {
        var cItem = GameController.instance.playerController.inventory.GetItemInSlot(GameController.instance.playerController.inventory.currentSlot);
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
