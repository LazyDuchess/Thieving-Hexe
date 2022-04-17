using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOptimization : MonoBehaviour
{
    public float distanceMax = 50f;
    public float itemFreezeDistance = 50f;
    public float characterFreezeDistance = 50f;
    public List<GameObject> lodCenters;

    bool checkTooFarAway(Vector3 position, float distanceMax)
    {
        foreach(var element in lodCenters)
        {
            var dist = Vector3.Distance(new Vector3(element.transform.position.x,0f,element.transform.position.z),position);
            if (dist < distanceMax)
                return false;
        }
        return true;
    }
    void CullingStep()
    {
        var ites = GameController.GetItems();
        var dirty = false;
        foreach(var element in ites)
        {
            if (element.owner == null)
            {
                var toPos = new Vector3(element.dropObject.transform.position.x, 0f, element.dropObject.transform.position.z);
                var dist = checkTooFarAway(toPos, itemFreezeDistance);
                if (dist && element.dropObject.activeSelf)
                {
                    element.dropObject.SetActive(false);
                    dirty = true;
                }
                if (!dist && !element.dropObject.activeSelf)
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
                var toPos = new Vector3(element.transform.position.x, 0f, element.transform.position.z);
            var dist = checkTooFarAway(toPos, characterFreezeDistance);
            if (dist && !element.culled)
                {
                    element.Optimize();
                    dirty = true;
                }
                if (!dist && element.culled)
                {
                    element.Unoptimize();
                    dirty = true;
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
                var dist = checkTooFarAway(roomPos, radius);
                if (dist && element.instance.activeSelf)
                {
                    dirty = true;
                    element.instance.SetActive(false);
                }
                if (dist && !element.instance.activeSelf)
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
