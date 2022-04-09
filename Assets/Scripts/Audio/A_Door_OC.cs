using UnityEngine;

public class A_Door_OC : MonoBehaviour
{
    public DoorComponent doorComponent;

    private bool neverDone;
    private bool allowClose;


    public void Start()
    {
        neverDone = false;
        if (doorComponent.open == false)
            allowClose = true;
    }

    public void Update()
    {

        if (doorComponent.open == true)
            if (neverDone)
            {
                AkSoundEngine.PostEvent("Play_Obj_Door_Open", gameObject);
                allowClose = true;
            }
        if (allowClose == true)
        {
            if (doorComponent.open == false)
            {
                AkSoundEngine.PostEvent("Play_Obj_door_Close", gameObject);
            }
        }
    }
}
