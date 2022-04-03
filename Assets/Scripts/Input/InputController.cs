using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Invalid target - no player controller. todo - lerp back to zero?
        if (!GameController.instance.playerController)
            return;
        GameController.instance.playerController.movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    }
}
