using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Door_OC : MonoBehaviour
{
    public DoorComponent doorComponent;

    public void Update()
    {
        if (doorComponent.open)
            AkSoundEngine.PostEvent("Play_Obj_Door_Open", gameObject);
        else if (!doorComponent.open)
            AkSoundEngine.PostEvent("Play_Obj_door_Close", gameObject);
    }
}
