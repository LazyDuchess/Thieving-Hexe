using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class FirstPersonCommand : Command
    {
        public override string Description()
        {
            return "Puts the camera in the player's head.";
        }

        public override void Execute(List<string> args)
        {
            GameController.instance.firstPerson = true;
            var cam = GameCamera.instance;
            var camGameObject = cam.gameObject;
            cam.enabled = false;
            var fpCam = camGameObject.AddComponent<FPSCamera>();
            fpCam.head = GameController.instance.playerController.mesh.GetComponent<WitchAnimationComponent>().lookAtHead;
            var cameruh = camGameObject.GetComponent<Camera>();
            cameruh.fieldOfView = 90f;
            cam.xRay.GetComponent<Camera>().fieldOfView = 90f;
        }

        public override string Name()
        {
            return "firstperson";
        }
    }
}