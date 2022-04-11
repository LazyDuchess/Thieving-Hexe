using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorFace { Left, Right, Forward, Backward };
public class DoorComponent : InteractableComponent
{
    public GameObject doorAnimationObject;
    public float doorLerp = 5f;

    public float doorOffsetOpen = -5f;

    public GameObject doorClosedObject;
    public DoorFace facing;
    public bool open = false;
    public bool empty = true;
    public int id = 0;

    public delegate void VoidEvent();
    public VoidEvent openDoorEvent;
    public VoidEvent closeDoorEvent;

    Vector3 closedPosition = Vector3.zero;
    Vector3 openPosition = Vector3.zero;

    private void Update()
    {
        var fromPos = doorAnimationObject.transform.position;
        var targetPos = openPosition;
        if (open == false)
            targetPos = closedPosition;
        doorAnimationObject.transform.position = Vector3.Lerp(fromPos, targetPos, doorLerp * Time.deltaTime);
    }

    private void Awake()
    {
        VerifyDoor();
    }

    void VerifyDoor()
    {
        if (closedPosition == Vector3.zero)
        {
            closedPosition = doorAnimationObject.transform.position;
            openPosition = doorAnimationObject.transform.position + (doorOffsetOpen * Vector3.up);
        }
    }

    public void CloseDoor(bool instant = true)
    {
        VerifyDoor();
        if (open)
        {
            doorClosedObject.SetActive(true);
            open = false;
            if (instant)
            {
                doorAnimationObject.transform.position = closedPosition;
            }
            if (closeDoorEvent != null)
                closeDoorEvent.Invoke();
        }
    }

    public override bool Interactable()
    {
        return empty;
    }

    public override bool Test(CharacterComponent actor)
    {
        if (base.Test(actor))
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
            var result = recursionPiece.instantiate(roomComp.gameObject, DungeonController.instance.level.transform, false);
        }
        else
        {
            UINotification.instance.Show("This door won't budge.", 3f);
        }
    }

    public void OpenDoor(bool instant = true)
    {
        VerifyDoor();
        if (!open && !empty)
        {
            doorClosedObject.SetActive(false);
            open = true;
            if (instant)
            {
                doorAnimationObject.transform.position = openPosition;
            }
            if (openDoorEvent != null)
                openDoorEvent.Invoke();
        }
    }



    public void ToggleDoor(bool instant = true)
    {
        VerifyDoor();
        if (open)
            CloseDoor(instant);
        else
            OpenDoor(instant);
    }
}


