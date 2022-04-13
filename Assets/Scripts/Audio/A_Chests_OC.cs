using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Chests_OC : MonoBehaviour
{
    GameObject chestObject;
    [SerializeField] AK.Wwise.Event playChestOpen;


    public void Start()
    {
        chestObject = gameObject;
        var chestComponent = GetComponent<ChestComponent>();
        chestComponent.openChestEvent += PlayOpenChest;
     
    }

    public void PlayOpenChest()
    {
        if (GameController.GetAudioHacks())
        {
            playChestOpen.Post(GameCamera.instance.gameObject);
            playChestOpen.Post(GameCamera.instance.gameObject);
        }
        else
            playChestOpen.Post(gameObject);
    }
}

