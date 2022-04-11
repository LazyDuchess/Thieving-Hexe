using UnityEngine;

public class A_Door_OC : MonoBehaviour
{
    GameObject door;

    public void Start()
    {
        door = GetComponent<GameObject>();
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
