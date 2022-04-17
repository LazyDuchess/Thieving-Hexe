using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    float hatPrevious = 0f;
    float hatCurrent = 0f;

    float cyclePrevious = 0f;
    float cycleCurrent = 0f;

    public bool enableController = true;
    public bool enableKeyboard = true;

    public bool playerTwo = false;

    bool dashing = false;

    PlayerController getTargetPlayer()
    {
        if (playerTwo)
            return GameController.instance.coopPlayer;
        return GameController.instance.playerController;
    }

    void HatLoop()
    {
        var hat = Input.GetAxisRaw("Inventory Joystick");
        var hatClamped = 0f;
        if (hat > 0f)
            hatClamped = 1f;
        if (hat < 0f)
            hatClamped = -1f;
        if (hatClamped == hatPrevious)
            hatClamped = 0f;
        else
        {
            hatPrevious = hatClamped;
        }
        hatCurrent = hatClamped;
        var cycle = Input.GetAxisRaw("Inventory Cycle");
        var cycleClamped = 0f;
        if (cycle > 0f)
            cycleClamped = 1f;
        if (cycle < 0f)
            cycleClamped = -1f;
        if (cycleClamped == cyclePrevious)
            cycleClamped = 0f;
        else
            cyclePrevious = cycleClamped;
        cycleCurrent = cycleClamped;
    }
    // Update is called once per frame

    void KeyboardUpdate()
    {
        
        if (!GameController.instance.firstPerson)
            getTargetPlayer().movementVector += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        else
        {
            var frontHeading = FPSCamera.instance.GetRotation() * Vector3.forward;
            var rightHeading = FPSCamera.instance.GetRotation() * Vector3.right;
            getTargetPlayer().movementVector = Vector3.zero;
            getTargetPlayer().movementVector -= Input.GetAxisRaw("Vertical") * frontHeading;
            getTargetPlayer().movementVector -= Input.GetAxisRaw("Horizontal") * rightHeading;
        }
        if (getTargetPlayer().CanUseInventory())
        {
            var scroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                var newSlot = getTargetPlayer().inventory.currentSlot;
                if (scroll > 0f)
                    newSlot += 1;
                else
                    newSlot -= 1;
                if (newSlot < 0)
                    newSlot = getTargetPlayer().inventory.maxCapacity - 1;
                if (newSlot >= getTargetPlayer().inventory.maxCapacity)
                    newSlot = 0;
                getTargetPlayer().inventory.SwitchSlot(newSlot);
            }
            var initialInventorySlotKey = KeyCode.Alpha1;
            for (var i = 0; i < getTargetPlayer().inventory.maxCapacity; i++)
            {
                if (Input.GetKeyDown(initialInventorySlotKey + i))
                {
                    getTargetPlayer().inventory.SwitchSlot(i);
                }
            }
            if (Input.GetButtonDown("Drop"))
                getTargetPlayer().inventory.DropCurrentItem();

            if (!getTargetPlayer().ActionBusy())
            {
                if (getTargetPlayer().GetPrimaryFireMode() == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire1"))
                        getTargetPlayer().Primary();
                    if (Input.GetButtonUp("Fire1"))
                        getTargetPlayer().PrimaryEnd();
                }
                if (getTargetPlayer().GetSecondaryFireMode() == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire2"))
                        getTargetPlayer().Secondary();
                    if (Input.GetButtonUp("Fire2"))
                        getTargetPlayer().SecondaryEnd();
                }
                if (getTargetPlayer().GetPrimaryFireMode() == FireMode.Auto)
                {
                    if (Input.GetButton("Fire1"))
                        getTargetPlayer().Primary();
                }
                if (getTargetPlayer().GetSecondaryFireMode() == FireMode.Auto)
                {
                    if (Input.GetButton("Fire2"))
                        getTargetPlayer().Secondary();
                }
            }
        }
        if (Input.GetButton("Dash"))
            dashing = true;
        if (!getTargetPlayer().ActionBusy())
        {
            if (Input.GetButtonDown("Use"))
                getTargetPlayer().Interact();
        }
    }

    void ControllerUpdate()
    {
        HatLoop();
        getTargetPlayer().movementVector += new Vector3(Input.GetAxisRaw("Horizontal Controller"), 0f, Input.GetAxisRaw("Vertical Controller"));
        if (getTargetPlayer().CanUseInventory())
        {
            var cycle = cycleCurrent;
            if (cycle != 0f)
            {
                var newSlot = getTargetPlayer().inventory.currentSlot;
                newSlot += 1;
                if (newSlot >= getTargetPlayer().inventory.maxCapacity)
                    newSlot = 0;
                while (getTargetPlayer().inventory.GetItemInSlot(newSlot) == null)
                {
                    newSlot += 1;
                    if (newSlot >= getTargetPlayer().inventory.maxCapacity)
                        newSlot = 0;
                }
                getTargetPlayer().inventory.SwitchSlot(newSlot);
            }
            var scroll = hatCurrent;
            if (scroll != 0f)
            {
                var newSlot = getTargetPlayer().inventory.currentSlot;
                if (scroll > 0f)
                    newSlot += 1;
                else
                    newSlot -= 1;
                if (newSlot < 0)
                    newSlot = getTargetPlayer().inventory.maxCapacity - 1;
                if (newSlot >= getTargetPlayer().inventory.maxCapacity)
                    newSlot = 0;
                getTargetPlayer().inventory.SwitchSlot(newSlot);
            }
            if (Input.GetButtonDown("Drop Controller"))
                getTargetPlayer().inventory.DropCurrentItem();

            if (!getTargetPlayer().ActionBusy())
            {
                if (getTargetPlayer().GetPrimaryFireMode() == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire1 Controller"))
                        getTargetPlayer().Primary();
                    if (Input.GetButtonUp("Fire1 Controller"))
                        getTargetPlayer().PrimaryEnd();
                }
                if (getTargetPlayer().GetSecondaryFireMode() == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire2 Controller"))
                        getTargetPlayer().Secondary();
                    if (Input.GetButtonUp("Fire2 Controller"))
                        getTargetPlayer().SecondaryEnd();
                }
                if (getTargetPlayer().GetPrimaryFireMode() == FireMode.Auto)
                {
                    if (Input.GetButton("Fire1 Controller"))
                        getTargetPlayer().Primary();
                }
                if (getTargetPlayer().GetSecondaryFireMode() == FireMode.Auto)
                {
                    if (Input.GetButton("Fire2 Controller"))
                        getTargetPlayer().Secondary();
                }
            }
        }
        if (Input.GetAxisRaw("Dash Joystick") > 0f)
            dashing = true;
        if (!getTargetPlayer().ActionBusy())
        {
            if (Input.GetButtonDown("Use Controller"))
                getTargetPlayer().Interact();
        }
    }

    void Update()
    {
        dashing = false;
        if (PauseController.paused)
            return;
        //Invalid target - no player controller. todo - lerp back to zero?
        if (!getTargetPlayer())
            return;
        getTargetPlayer().movementVector = Vector3.zero;
        if (!GameController.instance.controlEnabled || GameController.GetBusy())
            return;
        
        if (enableKeyboard)
            KeyboardUpdate();
        if (enableController)
            ControllerUpdate();
        if (dashing)
            getTargetPlayer().SetDashing(true);
        else
            getTargetPlayer().SetDashing(false);
    }
}
