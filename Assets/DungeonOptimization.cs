using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOptimization : MonoBehaviour
{
    public float distanceMax = 20f;
    void CullingStep()
    {
        foreach(var element in DungeonController.instance.dungeonLevel.pieces)
        {
            if (element.instance)
            {
                var roomComp = element.instance.GetComponent<RoomComponent>();
                var radius = roomComp.xSize * DungeonPiece.size;
                if (roomComp.ySize > roomComp.xSize)
                    radius = roomComp.ySize * DungeonPiece.size;
                radius += distanceMax;
                var camPos = new Vector3(transform.position.x, 0f, transform.position.z);
                var center = element.instance.transform.position + (Vector3.right * (roomComp.xSize * DungeonPiece.size)) + (Vector3.forward * (roomComp.ySize * DungeonPiece.size));
                var roomPos = new Vector3(center.x, 0f, center.z);
                var dist = Vector3.Distance(camPos, roomPos);
                if (dist >= radius && element.instance.activeSelf)
                    element.instance.SetActive(false);
                if (dist < radius && !element.instance.activeSelf)
                    element.instance.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CullingStep();
    }
}
