using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterComponent
{
    ItemComponent drewItem;
    public Inventory inventory = new Inventory();

    //For animating
    public bool backwards = false;

    private Vector3 aimLocation = Vector3.zero;

    public InteractableComponent currentInteractable;

    public FireMode GetPrimaryFireMode()
    {
        var ite = inventory.GetItemInSlot(inventory.currentSlot);

        if (ite != null)
        {
            return ite.primaryFireMode;
        }
        return FireMode.Single;
    }

    public FireMode GetSecondaryFireMode()
    {
        var ite = inventory.GetItemInSlot(inventory.currentSlot);

        if (ite != null)
        {
            return ite.secondaryFireMode;
        }
        return FireMode.Single;
    }

    public void Primary()
    {
        if (!IsAlive())
            return;
        var ite = inventory.GetItemInSlot(inventory.currentSlot);
        
        if (ite != null)
        {
            if (ite.CanFirePrimary())
                ite.Primary();
        }
    }

    public void Secondary()
    {
        if (!IsAlive())
            return;
        var ite = inventory.GetItemInSlot(inventory.currentSlot);
        if (ite != null)
        {
            if (ite.CanFireSecondary())
                ite.Secondary();
        }
    }

    public void PrimaryEnd()
    {
        if (!IsAlive())
            return;
        var ite = inventory.GetItemInSlot(inventory.currentSlot);
        if (ite != null)
        {
            if (ite.holdTime > 0f)
                ite.PrimaryEnd();
        }
    }

    public void SecondaryEnd()
    {
        if (!IsAlive())
            return;
        var ite = inventory.GetItemInSlot(inventory.currentSlot);
        if (ite != null)
        {
            if (ite.holdTime > 0f)
                ite.SecondaryEnd();
        }
    }

    public void Interact()
    {
        if (currentInteractable == null)
            return;
        if (!IsAlive())
            return;
        currentInteractable.Interact(this);
    }

    private void InteractLoop()
    {
        currentInteractable = null;
        if (!IsAlive())
            return;
        InteractableComponent lastInteractable = null;
        var lastDistance = 0f;
        var allInteractables = GameController.GetInteractables();
       // var allInteractables = FindObjectsOfType<InteractableComponent>();
        foreach(var element in allInteractables)
        {
            if (element.Interactable())
            {
                
                    var overlaps = Physics.OverlapSphere(element.triggerPosition(), element.interactionRadius);
                    foreach (var element2 in overlaps)
                    {
                        var me = element2.GetComponent<PlayerController>();
                        if (me)
                        {
                            if (me == this)
                            {
                            if (element.Test(this))
                            {
                                var dist = Vector3.Distance(transform.position, element.triggerPosition());
                                if(lastInteractable == null)
                                {
                                    lastInteractable = element;
                                    lastDistance = dist;
                                }
                                else
                                {
                                    if (dist < lastDistance)
                                    {
                                        lastInteractable = element;
                                        lastDistance = dist;
                                    }
                                }
                            }
                            }
                        }
                    }
            }
        }
        currentInteractable = lastInteractable;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
        base.Start();
        DummyAim();
        inventory.onSwitchItem += Inventory_Draw;
        inventory.onAddItemEx += Inventory_Add;
    }

    public void UseCurrentInventory()
    {
        inventory.UseItem(inventory.currentSlot);
    }

    public bool CanUseInventory()
    {
        if (!IsAlive())
            return false;
        if (currentAction != null)
        {
            if (!currentAction.useInventory)
                return false;
        }
        return true;
    }

    public override void OnDeath(Damage killingDamage)
    {
        base.OnDeath(killingDamage);
        for(var i=0;i<inventory.maxCapacity;i++)
        {
            inventory.DropItem(i);
        }
    }

    void Inventory_Add(ItemComponent item)
    {
        item.owner = this;
    }

    void Inventory_Draw()
    {
        if (drewItem != null)
        {
            drewItem.Holster();
        }
        var itm = inventory.GetItemInSlot(inventory.currentSlot);
        if (itm != null)
        {
            itm.owner = this;
            itm.Draw();
        }
        else
        {
            SendEvent("Unarmed");
        }
        drewItem = itm;
    }

    void DummyAim()
    {
        aimLocation = transform.position + transform.forward * 5f;
    }

    public Vector3 GetAimHeading()
    {
        return (aimLocation - transform.position).normalized;
    }

    public Vector3 GetAimHeadingFlat()
    {
        var loc = aimLocation - transform.position;
        return new Vector3(loc.x, 0f, loc.z).normalized;
    }

    public Vector3 GetAim()
    {
        return aimLocation;
    }

    public void SetAim(Vector3 location)
    {
        aimLocation = location;
    }

    protected override void RotateCharacter()
    {
        return;
    }

    protected override void Update()
    {
        base.Update();
        if (IsAlive())
        {
            InteractLoop();
            Quaternion targetRotation;
            backwards = false;

            var vel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            var targetAim = new Vector3(aimLocation.x, mesh.transform.position.y, aimLocation.z);
            var target = (targetAim - mesh.transform.position).normalized;

            var aimQuaternion = Quaternion.LookRotation(target);
            var movementQuaternion = Quaternion.identity;
            var backwardsMovementQuaternion = Quaternion.identity;

            if (vel != Vector3.zero)
            {
                movementQuaternion = Quaternion.LookRotation(vel.normalized);
                backwardsMovementQuaternion = Quaternion.LookRotation(-vel.normalized);
            }


            var forwardMovement = movementQuaternion * Vector3.forward;

            if (vel.magnitude > Vector3.kEpsilon)
            {
                var heading = Vector3.Dot(forwardMovement, target);
                if (heading < -0.2f)
                {
                    backwards = true;
                    targetRotation = backwardsMovementQuaternion;
                }
                else
                {
                    targetRotation = movementQuaternion;
                }
            }
            else
            {
                targetRotation = aimQuaternion;
            }
            mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, targetRotation, rotationLerp * Time.deltaTime);
        }
    }
}
