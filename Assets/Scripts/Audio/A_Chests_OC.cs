using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Chests_OC : MonoBehaviour
{
    GameObject chestObject;

    public void Start()
    {
        chestObject = GetComponent<GameObject>();  
        GameEventsController.openChestEvent += PlayOpenChest;
        //GameEventsController.openCloseEvent += PlayClosChest;
    }

    public void PlayOpenChest()
    {
        AkSoundEngine.PostEvent("Obj_Door_Open", chestObject);
    }
}

