using UnityEngine;

public class A_Door_OC : MonoBehaviour
{
    GameObject door;

    public void Start()
    {
        door = GetComponent<GameObject>();
        GameEventsController.roomsOpenEvent += PlayOpenDoor;
        GameEventsController.roomsCloseEvent += PlayCloseDoor;
    }

     void PlayOpenDoor()
    {
            AkSoundEngine.PostEvent("Obj_Door_Open", door);
    }
     void PlayCloseDoor()
    {
        AkSoundEngine.PostEvent("Obj_Door_Close", door);
    }
}
