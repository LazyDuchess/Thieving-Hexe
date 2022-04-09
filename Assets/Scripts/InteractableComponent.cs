using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType { Generic, PickUp, Collect, Open };
public abstract class InteractableComponent : MonoBehaviour
{
    public InteractionType interactionType = InteractionType.PickUp;
    public float interactionRadius = 1.5f;

    public abstract void Interact(CharacterComponent actor);

    public virtual bool Interactable()
    {
        return true;
    }

    public virtual Vector3 triggerPosition()
    {
        return transform.position;
    }
}
