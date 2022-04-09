using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorFace { Left, Right, Forward, Backward };
public class DoorComponent : MonoBehaviour
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


