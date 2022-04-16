using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultItem : MonoBehaviour
{
    public bool singleton = true;
    public ItemComponent item;
    private void Start()
    {
        item.gameObject.SetActive(false);
        /*
        var inventory = GameController.instance.playerController.inventory;
            if (singleton)
            {
                if (!inventory.HasItem(item))
                {
                    inventory.AddItem(item);
                }
            }
            else
                inventory.AddItem(item);*/
    }

    public void Trigger(Inventory inventory)
    {
        var gnewItem = Instantiate(item.gameObject);
        gnewItem.SetActive(true);
        var newItem = gnewItem.GetComponent<ItemComponent>();
        if (singleton)
        {
            if (!inventory.HasItem(newItem))
            {
                inventory.AddItem(newItem);
            }
        }
        else
            inventory.AddItem(newItem);
    }
}
