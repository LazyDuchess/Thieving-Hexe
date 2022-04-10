using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentInteractable : InteractableComponent
{
    public GameObject fragment;

    public override bool Interactable()
    {
        if (DungeonController.instance.dungeonState.wayOut)
            return false;
        return true;
    }
    public override void Interact(CharacterComponent actor)
    {
        fragment.SetActive(false);
        DungeonController.instance.SetWayOut();
        GameEventsController.CollectFragment();
        UINotification.instance.Show("Now escape the way you came in!", 3f);
    }
}
