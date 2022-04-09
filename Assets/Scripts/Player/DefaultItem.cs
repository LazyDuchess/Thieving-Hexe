using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultItem : MonoBehaviour
{
    public bool singleton = true;
    public ItemComponent item;
    private void Start()
    {
        var inventory = GameController.instance.playerController.inventory;
            if (singleton)
            {
                if (!inventory.HasItem(item))
                {
                    inventory.AddItem(item);
                }
            }
            else
                inventory.AddItem(item);
    }
}
