using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType { Generic, PickUp, Collect, Open, Torch };
public abstract class InteractableComponent : MonoBehaviour
{
    public Transform interactionOrigin;
    public InteractionType interactionType = InteractionType.PickUp;
    public float interactionRadius = 1.5f;

    public virtual void Awake()
    {
        GameController.dirtyInteractables();
    }

    public virtual void OnDestroy()
    {
        GameController.dirtyInteractables();
    }
    public abstract void Interact(CharacterComponent actor);

    public virtual bool Test(CharacterComponent actor)
    {
        var fromPoint = actor.transform.position + (Vector3.up * actor.height);
        var toPoint = interactionOrigin.position;
        var off = 0.4f;
        var rayOrigin = fromPoint;
        var rayHeading = (toPoint - fromPoint).normalized;
        var rayDistance = Vector3.Distance(fromPoint, toPoint);
        rayOrigin += rayHeading * off;
        rayDistance -= off * 2f;
        Ray ray = new Ray(rayOrigin, rayHeading);
#if UNITY_EDITOR
        Debug.DrawLine(rayOrigin, rayOrigin + (rayDistance * rayHeading), Color.red, 10f);
#endif
        var cast = Physics.RaycastAll(ray, rayDistance);
        var notValid = true;
        foreach (var element in cast)
        {
            notValid = false;
            var el = element.collider;
            var allCharacters = new List<CharacterComponent>(element.collider.GetComponents<CharacterComponent>());
            allCharacters.AddRange(element.collider.GetComponentsInChildren<CharacterComponent>());
            allCharacters.AddRange(element.collider.GetComponentsInParent<CharacterComponent>());
            foreach (var chara in allCharacters)
            {
                if (chara)
                {
                    if (chara == actor)
                        notValid = true;
                }
            }
            var allItems = new List<InteractableComponent>(element.collider.GetComponents<InteractableComponent>());
            allItems.AddRange(element.collider.GetComponentsInChildren<InteractableComponent>());
            allItems.AddRange(element.collider.GetComponentsInParent<InteractableComponent>());
            foreach (var itemTest in allItems)
            {
                if (itemTest)
                {
                    if (itemTest == this)
                        notValid = true;
                }
            }
        }
        return notValid;
    }

    public virtual bool Interactable()
    {
        return true;
    }

    public virtual Vector3 triggerPosition()
    {
        return interactionOrigin.position;
    }
}
