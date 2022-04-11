using UnityEngine;

public class A_Door_OC : MonoBehaviour
{

    [SerializeField] AK.Wwise.Event playDoorOpen;
    [SerializeField] AK.Wwise.Event playDoorClose;
    public DoorComponent doorScript;

    public void Start()
    {
        doorScript.openDoorEvent += PlayOpenDoor;
        doorScript.closeDoorEvent += PlayCloseDoor;
        //GameEventsController.roomsOpenEvent += PlayOpenDoor;
        //GameEventsController.roomsCloseEvent += PlayCloseDoor;
    }

     void PlayOpenDoor()
    {
            playDoorOpen.Post(gameObject);
    }
     void PlayCloseDoor()
    {
        playDoorClose.Post(gameObject);
    }
}
