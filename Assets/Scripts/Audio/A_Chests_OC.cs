using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Chests_OC : MonoBehaviour
{
    GameObject chestObject;
    [SerializeField] AK.Wwise.Event playChestOpen;


    public void Start()
    {
        chestObject = GetComponent<GameObject>();  
        GameEventsController.openChestEvent += PlayOpenChest;
     
    }

    public void PlayOpenChest()
    {
        playChestOpen.Post(gameObject);
    }
}

