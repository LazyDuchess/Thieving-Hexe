using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickUpNameComponent : MonoBehaviour
{
    public Text tooltip;
    // Start is called before the first frame update
    void Start()
    {
        //GameController.instance.playerController.inventory.onSwitchItem += Show;
        GameController.instance.OnInventorySwitchEvent += Show;
    }

    public void Show()
    {
        var cItem = GameController.instance.playerController.inventory.GetItemInSlot(GameController.instance.playerController.inventory.currentSlot);
        CancelInvoke();
        if (cItem != null)
        {
            tooltip.text = GameController.instance.playerController.inventory.GetItemInSlot(GameController.instance.playerController.inventory.currentSlot).itemName;
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
