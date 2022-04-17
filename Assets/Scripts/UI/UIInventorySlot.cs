using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public int slotID = 0;
    public GameObject emptyObject;
    public GameObject occupiedObject;
    public GameObject quantityObject;
    public Text quantityText;
    public GameObject selectedObject;
    public GameObject itemIconObject;
    public Image itemIcon;

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
        UpdateSlot();
        if (!playerTwo)
        {
            GameController.instance.OnInventorySwitchEvent += UpdateSlot;
            GameController.instance.OnInventoryUpdateEvent += UpdateSlot;
        }
        else
        {
            GameController.instance.OnInventorySwitchEventCoop += UpdateSlot;
            GameController.instance.OnInventoryUpdateEventCoop += UpdateSlot;
        }
    }

    void UpdateSlot()
    {
        emptyObject.SetActive(false);
        occupiedObject.SetActive(false);
        quantityObject.SetActive(false);
        selectedObject.SetActive(false);
        itemIconObject.SetActive(false);
        var ite = getTargetPlayer().inventory.GetItemInSlot(slotID);
        if (ite != null)
        {
            occupiedObject.SetActive(true);
            if (ite.stackable)
            {
                quantityObject.SetActive(true);
                quantityText.text = ite.amount.ToString();
            }
            if (ite.inventorySprite != null)
            {
                itemIconObject.SetActive(true);
                itemIcon.sprite = ite.inventorySprite;
            }
        }
        else
        {
            emptyObject.SetActive(true);
        }
        if (getTargetPlayer().inventory.currentSlot == slotID)
            selectedObject.SetActive(true);
    }
}
