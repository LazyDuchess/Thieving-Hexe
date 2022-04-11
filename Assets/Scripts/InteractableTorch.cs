using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTorch : InteractableComponent
{
    public bool taken = false;
    public GameObject wallTorchObject;
    public GameObject torchItem;
    public override bool Interactable()
    {
        if (taken)
            return false;
        return true;
    }

    public override void Interact(CharacterComponent actor)
    {
        var play = actor.GetComponent<PlayerController>();
        if (play)
        {
            if (play.inventory.FreeSlots())
            {
                var wallTorchItem = Instantiate(torchItem);
                var torchItemComponent = wallTorchItem.GetComponent<ItemComponent>();
                wallTorchObject.SetActive(false);
                play.inventory.AddItem(torchItemComponent);
                taken = true;
            }
            else
            {
                UINotification.instance.Show("I don't have the space for a torch in my inventory.", 3f);
            }
        }
    }
}
