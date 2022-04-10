using UnityEngine;

public class A_Door_OC : MonoBehaviour
{
    public GameObject door;

    public void Start()
    {
        //GameEventsController.openDoorEvent += PlayOpenDoor;
        //GameEventsController.openCloseEvent += PlayCloseDoor;
    }

    public void PlayOpenDoor()
    {
            AkSoundEngine.PostEvent("Obj_Door_Open", door);
    }
    public void PlayCloseDoor()
    {
        AkSoundEngine.PostEvent("Obj_Door_Close", door);
    }
}
