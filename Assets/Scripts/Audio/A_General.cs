using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_General : MonoBehaviour
{
    #region Public
    [SerializeField] public DoorComponent doorComponent;
    
    #endregion

    #region Private
        [SerializeField] private AK.Wwise.Event doorOpen;
    [SerializeField] private AK.Wwise.Event doorClose;
    #endregion

    //open door sound
    public void PlayDoorOpen()
    {
        doorOpen.Post(gameObject);
    }

}
