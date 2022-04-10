using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorFace { Left, Right, Forward, Backward };
public class DoorComponent : InteractableComponent
{
    public GameObject doorClosedObject;
    public DoorFace facing;
    public bool open = false;
    public bool empty = true;
    public int id = 0;
    public void CloseDoor()
    {
        if (open)
        {
            doorClosedObject.SetActive(true);
            open = false;
        }
    }

    public override bool Interactable()
    {
        return empty;
    }

    public override bool Test(CharacterComponent actor)
    {
        var playa = actor.GetComponent<PlayerController>();
        if (playa)
        {
            var curItem = playa.inventory.GetCurrentItem();
            if (curItem)
            {
                if (playa.inventory.GetCurrentItem().itemUID == "key")
                    return true;
            }
        }
        return false;
    }

    public override void Interact(CharacterComponent actor)
    {
        var playa = actor.GetComponent<PlayerController>();
        if (!playa)
            return;
        var roomComp = transform.parent.GetComponent<RoomComponent>();
        if (!roomComp)
            return;
        var parentPiece = roomComp.parentPiece;
        if (parentPiece == null)
            return;
        var recursionPiece = DungeonController.instance.Recurse(parentPiece, this);
        if (recursionPiece != null)
        {
            playa.UseCurrentInventory();
            GameEventsController.OpenDoor();
            var result = recursionPiece.instantiate(roomComp.gameObject, DungeonController.instance.level.transform);
        }
        else
        {
            UINotification.instance.Show("This door won't budge.", 3f);
        }
    }

    public void OpenDoor()
    {
        if (!open && !empty)
        {
            doorClosedObject.SetActive(false);
            open = true;
        }
    }

    public void ToggleDoor()
    {
        if (open)
            CloseDoor();
        else
            OpenDoor();
    }
}
