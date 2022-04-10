using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Chests_OC : MonoBehaviour
{
    public GameObject chest;

    public void Start()
    {
        GameEventsController.openChestEvent += PlayOpenChest;
        //GameEventsController.openCloseEvent += PlayClosChest;
    }

    public void PlayOpenChest()
    {
        AkSoundEngine.PostEvent("Obj_Door_Open", chest);
    }
}

