using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAddItemResult
{
    public bool success = false;
    public int remainingAmount = 0;
}
public class Inventory
{
    public int maxCapacity = 5;
    public List<ItemComponent> items = new List<ItemComponent>();
    public int currentSlot = 0;
    public delegate void EventListener();
    public delegate void ItemListener(ItemComponent item);
    public EventListener onAddItem;
    public EventListener onDropItem;
    public EventListener onSwitchItem;
    public ItemListener onAddItemEx;

    public ItemComponent GetItemInSlot(int slot)
    {
        return items[slot];
    }

    public ItemComponent GetCurrentItem()
    {
        return GetItemInSlot(currentSlot);
    }

    public bool HasItem(string uid)
    {
        foreach (var element in items)
        {
            if (element != null)
            {
                if (element.itemUID == uid)
                    return true;
            }
        }
        return false;
    }

    public bool HasItem(ItemComponent item)
    {
        foreach(var element in items)
        {
            if (element != null)
            {
                if (element.itemUID == item.itemUID)
                    return true;
            }
        }
        return false;
    }
    public void SwitchSlot(int slot)
    {
        if (slot == currentSlot)
            return;
        if (slot <= 0)
            slot = 0;
        if (slot >= maxCapacity)
            slot = maxCapacity-1;
        currentSlot = slot;
        if (onSwitchItem != null)
            onSwitchItem.Invoke();
    }

    public Inventory(int capacity)
    {
        maxCapacity = capacity;
        for (var i = 0; i < maxCapacity; i++)
        {
            items.Add(null);
        }
    }

    public Inventory()
    {
        for(var i=0;i<maxCapacity;i++)
        {
            items.Add(null);
        }
    }

    public void UseItem(int slot)
    {
        var item = items[slot];
        if (item != null)
        {
            item.amount -= 1;
            if (onDropItem != null)
                onDropItem.Invoke();
            if (!item.stackable || item.amount <= 0)
            {
                items[slot] = null;
                if (slot == currentSlot)
                {
                    if (onSwitchItem != null)
                        onSwitchItem.Invoke();
                }
            }
        }
    }

    public void DropCurrentItem()
    {
        DropItem(currentSlot);
    }

    public void DropItem(int slot)
    {
        var item = items[slot];
        if (item != null)
        {
            if (item.droppable)
            {
                items[slot] = null;
                item.Drop();
                if (onDropItem != null)
                    onDropItem.Invoke();
                if (slot == currentSlot)
                {
                    if (onSwitchItem != null)
                        onSwitchItem.Invoke();
                }
            }
        }
    }

    void InternalAddItem(ItemComponent item)
    {
        for(var i=0;i<items.Count;i++)
        {
            if (items[i] == null)
            {
                item.ToInventory(this);
                items[i] = item;
                if (onAddItem != null)
                    onAddItem.Invoke();
                if (onAddItemEx != null)
                    onAddItemEx.Invoke(item);
                if (i == currentSlot)
                {
                    if (onSwitchItem != null)
                        onSwitchItem.Invoke();
                }
                return;
            }
        }
    }

    public InventoryAddItemResult AddItem(ItemComponent item)
    {
        var result = new InventoryAddItemResult();
        var amountRemaining = item.amount;
        result.remainingAmount = amountRemaining;
        result.success = false;
        if (!item.stackable)
        {
            if (FreeSlots())
            {
                result.success = true;
                result.remainingAmount = 0;
                InternalAddItem(item);
                return result;
            }
            else
            {
                return result;
            }
        }
        else
        {
            var firstEmpty = -1;
            for(var i=0;i<items.Count;i++)
            {
                var element = items[i];
                if (element != null)
                {

                    if (element.itemUID == item.itemUID)
                {
                    if (element.amount < element.maxAmount)
                    {
                        var remainingSpace = element.maxAmount - element.amount;
                        if (remainingSpace >= amountRemaining)
                        {
                            element.amount += amountRemaining;
                            result.success = true;
                            result.remainingAmount = 0;
                                item.amount = 0;
                                if (onAddItem != null)
                                    onAddItem.Invoke();
                                GameObject.Destroy(item.gameObject);
                            return result;
                        }
                        else
                        {
                            element.amount = element.maxAmount;
                            amountRemaining -= remainingSpace;
                            result.success = true;
                            result.remainingAmount = amountRemaining;
                                item.amount = amountRemaining;
                                if (onAddItem != null)
                                    onAddItem.Invoke();
                            }
                    }
                }
                }
                else
                {
                    if (firstEmpty == -1)
                        firstEmpty = i;
                }
            }
            if (amountRemaining > 0 && firstEmpty != -1)
            {
                InternalAddItem(item);
                result.success = true;
                result.remainingAmount = 0;
            }
        }
        return result;
    }

    public bool CanFitItem(ItemComponent item)
    {
        if (FreeSlots())
            return true;
        if (!item.stackable)
            return false;
        foreach(var element in items)
        {
            if (element != null)
            {
                if (element.itemUID == item.itemUID)
                {
                    if (element.amount < element.maxAmount)
                        return true;
                }
            }
        }
        return false;
    }

    public bool FreeSlots()
    {
        foreach(var element in items)
        {
            if (element == null)
                return true;
        }
        return false;
    }
}
