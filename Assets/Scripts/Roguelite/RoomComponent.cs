using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFaceResult
{
    public float xOffset;
    public float yOffset;
}

public class RoomComponent : MonoBehaviour
{
    public int xSize = 0;
    public int ySize = 0;
    public bool outdoor = false;

    [HideInInspector]
    public DungeonPiece parentPiece;

    public void CloseDoors()
    {
        var allDoors = GetComponentsInChildren<DoorComponent>();
        foreach (var element in allDoors)
            element.CloseDoor();
    }

    public void OpenDoors()
    {
        var allDoors = GetComponentsInChildren<DoorComponent>();
        foreach (var element in allDoors)
            element.OpenDoor();
    }

    public PlayerSpawn getPlayerSpawn()
    {
        var allSpawns = GetComponentsInChildren<PlayerSpawn>();
        var index = Random.Range(0,allSpawns.Length);
        return allSpawns[index];
    }

    public void EnterRoom()
    {
        var allTriggers = GetComponentsInChildren<RoomTrigger>();
        foreach(var element in allTriggers)
        {
            element.OnPlayerEnter();
        }
    }

    public void EnterRoomWayOut()
    {
        var allTriggers = GetComponentsInChildren<RoomTrigger>();
        foreach (var element in allTriggers)
        {
            element.OnPlayerComeBack();
        }
    }

    public DoorFaceResult isCompatibleWithDoorFace(DoorFace face)
    {
        var result = new DoorFaceResult();
        var allFaces = GetComponentsInChildren<DoorComponent>();
        //Is This It?
        var strokes = false;
        foreach (var element in allFaces)
        {
            result.xOffset = element.transform.localPosition.x;
            result.yOffset = element.transform.localPosition.z;
            switch(element.facing)
            {
                case DoorFace.Backward:
                    if (face == DoorFace.Forward)
                    {
                        strokes = true;
                        result.yOffset += 5f;
                    }
                    break;
                case DoorFace.Forward:
                    if (face == DoorFace.Backward)
                    {
                        strokes = true;
                        result.yOffset -= 5f;
                    }
                    break;
                case DoorFace.Right:
                    if (face == DoorFace.Left)
                    {
                        strokes = true;
                        result.xOffset -= 5f;
                    }
                    break;
                case DoorFace.Left:
                    if (face == DoorFace.Right)
                    {
                        strokes = true;
                        result.xOffset += 5f;
                    }
                    break;
            }
            if (strokes)
                break;
        }
        if (strokes)
            return result;
        else
            return null;
    }
}
