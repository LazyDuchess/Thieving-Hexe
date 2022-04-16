using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (PauseController.paused)
            return;
        //Invalid target - no player controller. todo - lerp back to zero?
        if (!GameController.instance.playerController)
            return;
        if (!GameController.instance.controlEnabled || GameController.GetBusy())
        {
            GameController.instance.playerController.movementVector = Vector3.zero;
            return;
        }
        if (!GameController.instance.firstPerson)
            GameController.instance.playerController.movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        else
        {
            var frontHeading = FPSCamera.instance.GetRotation() * Vector3.forward;
            var rightHeading = FPSCamera.instance.GetRotation() * Vector3.right;
            GameController.instance.playerController.movementVector = Vector3.zero;
            GameController.instance.playerController.movementVector -= Input.GetAxisRaw("Vertical") * frontHeading;
            GameController.instance.playerController.movementVector -= Input.GetAxisRaw("Horizontal") * rightHeading;
        }
        if (GameController.instance.playerController.CanUseInventory())
        {
            var scroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                var newSlot = GameController.instance.playerController.inventory.currentSlot;
                if (scroll > 0f)
                    newSlot += 1;
                else
                    newSlot -= 1;
                if (newSlot < 0)
                    newSlot = GameController.instance.playerController.inventory.maxCapacity - 1;
                if (newSlot >= GameController.instance.playerController.inventory.maxCapacity)
                    newSlot = 0;
                GameController.instance.playerController.inventory.SwitchSlot(newSlot);
            }
            var initialInventorySlotKey = KeyCode.Alpha1;
            for (var i = 0; i < GameController.instance.playerController.inventory.maxCapacity; i++)
            {
                if (Input.GetKeyDown(initialInventorySlotKey + i))
                {
                    GameController.instance.playerController.inventory.SwitchSlot(i);
                }
            }
            if (Input.GetButtonDown("Drop"))
                GameController.instance.playerController.inventory.DropCurrentItem();

            if (!GameController.instance.playerController.ActionBusy())
            {
                if (GameController.instance.playerController.GetPrimaryFireMode() == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire1"))
                        GameController.instance.playerController.Primary();
                    if (Input.GetButtonUp("Fire1"))
                        GameController.instance.playerController.PrimaryEnd();
                }
                if (GameController.instance.playerController.GetSecondaryFireMode() == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire2"))
                        GameController.instance.playerController.Secondary();
                    if (Input.GetButtonUp("Fire2"))
                        GameController.instance.playerController.SecondaryEnd();
                }
                if (GameController.instance.playerController.GetPrimaryFireMode() == FireMode.Auto)
                {
                    if (Input.GetButton("Fire1"))
                        GameController.instance.playerController.Primary();
                }
                if (GameController.instance.playerController.GetSecondaryFireMode() == FireMode.Auto)
                {
                    if (Input.GetButton("Fire2"))
                        GameController.instance.playerController.Secondary();
                }
            }
        }
        if (Input.GetButton("Dash"))
            GameController.instance.playerController.SetDashing(true);
        else
            GameController.instance.playerController.SetDashing(false);
        if (!GameController.instance.playerController.ActionBusy())
        {
            if (Input.GetButtonDown("Use"))
                GameController.instance.playerController.Interact();
        }
    }
}
