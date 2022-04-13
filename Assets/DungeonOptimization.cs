using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOptimization : MonoBehaviour
{
    public float distanceMax = 50f;
    public float itemFreezeDistance = 50f;
    public float characterFreezeDistance = 50f;
    void CullingStep()
    {
        var camPos = new Vector3(transform.position.x, 0f, transform.position.z);
        var ites = GameController.GetItems();
        var dirty = false;
        foreach(var element in ites)
        {
            if (element.owner == null)
            {
                var toPos = new Vector3(element.dropObject.transform.position.x, 0f, element.dropObject.transform.position.z);
                var dist = Vector3.Distance(camPos, toPos);
                if (dist >= itemFreezeDistance && element.dropObject.activeSelf)
                {
                    element.dropObject.SetActive(false);
                    dirty = true;
                }
                if (dist < itemFreezeDistance && !element.dropObject.activeSelf)
                {
                    element.dropObject.SetActive(true);
                    dirty = true;
                }
            }
        }
        if (dirty)
            GameController.dirtyItems();
        dirty = false;
        var chars = GameController.GetCharacters();
        foreach(var element in chars)
        {
            if (element != GameController.instance.playerController)
            {
                var toPos = new Vector3(element.transform.position.x, 0f, element.transform.position.z);
                var dist = Vector3.Distance(camPos, toPos);
                if (dist >= characterFreezeDistance && element.gameObject.activeSelf)
                {
                    element.gameObject.SetActive(false);
                    dirty = true;
                }
                if (dist < characterFreezeDistance && !element.gameObject.activeSelf)
                {
                    element.gameObject.SetActive(true);
                    dirty = true;
                }

            }
        }
        if (dirty)
            GameController.dirtyCharacters();
        dirty = false;
        foreach (var element in DungeonController.instance.dungeonLevel.pieces)
        {
            if (element.instance)
            {
                var roomComp = element.instance.GetComponent<RoomComponent>();
                var radius = roomComp.xSize * DungeonPiece.size;
                if (roomComp.ySize > roomComp.xSize)
                    radius = roomComp.ySize * DungeonPiece.size;
                radius += distanceMax;
                var center = element.instance.transform.position + (Vector3.right * (roomComp.xSize * DungeonPiece.size)) + (Vector3.forward * (roomComp.ySize * DungeonPiece.size));
                var roomPos = new Vector3(center.x, 0f, center.z);
                var dist = Vector3.Distance(camPos, roomPos);
                if (dist >= radius && element.instance.activeSelf)
                {
                    dirty = true;
                    element.instance.SetActive(false);
                }
                if (dist < radius && !element.instance.activeSelf)
                {
                    dirty = true;
                    element.instance.SetActive(true);
                }
            }
        }
        if (dirty)
            GameController.dirtyInteractables();
    }

    // Update is called once per frame
    void Update()
    {
        CullingStep();
    }
}
