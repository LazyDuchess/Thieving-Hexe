using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode { Single, Auto };
public enum ItemType { Potion, Key, Other };
public class ItemComponent : InteractableComponent
{
    public string animation = "onehanded";
    public string itemUID = "wand";
    public string itemName = "Wand";
    public Sprite inventorySprite;
    public bool stackable = false;
    public bool droppable = false;
    public bool pickupable = true;
    public int amount = 1;
    public int maxAmount = 5;
    public GameObject holdObject;
    public GameObject dropObject;
    public CharacterComponent owner;
    public Inventory inventory;
    public ItemType itemType = ItemType.Other;

    public float holdTime = 0f;
    bool holding = false;

    float coolDownP = 0f;
    float coolDownS = 0f;

    public FireMode primaryFireMode = FireMode.Single;
    public FireMode secondaryFireMode = FireMode.Single;

    public bool noHoldAnimation = false;

    public virtual void Primary() {
        holding = true;
    }

    public virtual void PrimaryEnd() {
        holding = false;
    }

    public virtual void Secondary() { 
        holding = true;
    }

    public virtual void SecondaryEnd() {
        holding = false;
    }

    public override Vector3 triggerPosition()
    {
        if (dropObject != null)
            return dropObject.transform.position;
        return transform.position;
    }
    public override bool Interactable()
    {
        if (owner != null)
            return false;
        return true;
    }

    public override void Interact(CharacterComponent actor)
    {
        var playerActor = actor.GetComponent<PlayerController>();
        if (playerActor)
        {
            var prevAmount = this.amount;
            var result = playerActor.inventory.AddItem(this);
            if (result.remainingAmount == prevAmount)
            {
                UINotification.instance.Show(itemName + ": I couldn't fit it in my inventory.", 3f);
            }
            else
            {
                switch(itemType)
                {
                    case ItemType.Key:
                        GameEventsController.PickUpKey();
                        break;
                    case ItemType.Other:
                        GameEventsController.PickUpOther();
                        break;
                    case ItemType.Potion:
                        GameEventsController.PickUpPotion();
                        break;
                }
                if (result.remainingAmount > 0)
                    UINotification.instance.Show(itemName + ": I couldn't fit all of it in my inventory.", 3f);
            }
        }
    }

    public virtual void CoolDownPrimary(float duration)
    {
        coolDownP += duration;
    }

    public virtual void CoolDownSecondary(float duration)
    {
        coolDownS += duration;
    }

    public virtual bool CanFirePrimary()
    {
        if (coolDownP > 0f)
            return false;
        return true;
    }

    public virtual bool CanFireSecondary()
    {
        if (coolDownS > 0f)
            return false;
        return true;
    }

    public virtual void Update()
    {
        if (holding)
            holdTime += Time.deltaTime;
        else
            holdTime = 0f;
        if (coolDownP > 0f)
        {
            coolDownP -= Time.deltaTime;
        }
        if (coolDownS > 0f)
        {
            coolDownS -= Time.deltaTime;
        }
        if (coolDownP < 0f)
            coolDownP = 0f;
        if (coolDownS < 0f)
            coolDownS = 0f;
    }

    public override void Awake()
    {
        base.Awake();
        GameController.dirtyItems();
        transform.parent = null;
        if (holdObject != null)
            holdObject.SetActive(false);
    }

    private void Start()
    {
        if (owner == null)
            transform.parent = DungeonController.instance.level.transform;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        GameController.dirtyItems();
    }

    public virtual void ToInventory(Inventory inventory)
    {
        this.transform.parent = null;
        this.inventory = inventory;
        if (holdObject != null)
            holdObject.SetActive(false);
        if (dropObject != null)
            dropObject.SetActive(false);
    }

    public virtual void Draw()
    {
        owner.holding = this;
        if (holdObject != null)
            holdObject.SetActive(true);
        if (dropObject != null)
            dropObject.SetActive(false);
        if (noHoldAnimation == false)
            owner.SendEvent("Hold");
        else
            owner.SendEvent("Hold_Unarmed");
        owner.SendEvent(animation + "_Draw");
        holding = false;
        holdTime = 0f;
    }

    public virtual void Holster()
    {
        if (holdObject != null)
            holdObject.SetActive(false);
        holding = false;
        holdTime = 0f;
    }

    public virtual void SpawnDropped()
    {
        if (holdObject != null)
            holdObject.SetActive(false);
        if (dropObject != null)
        {
            dropObject.SetActive(true);
            dropObject.transform.position = owner.transform.position;
            dropObject.transform.rotation = owner.mesh.transform.rotation;
        }
        owner = null;
    }

    public virtual void Drop()
    {
        transform.SetParent(DungeonController.instance.level.transform);
        if (holdObject != null)
            holdObject.SetActive(false);
        if (dropObject != null)
        {
            dropObject.SetActive(true);
            dropObject.transform.position = owner.transform.position;
            dropObject.transform.rotation = owner.mesh.transform.rotation;
            dropObject.GetComponent<Rigidbody>().velocity = owner.mesh.transform.forward * 2f + owner.FlatVelocity();
            dropObject.GetComponent<Rigidbody>().angularVelocity = VectorUtil.RandomNormal() * 5f;
        }
        owner = null;
    }
}
