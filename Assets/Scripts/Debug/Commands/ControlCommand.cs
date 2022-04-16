using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class ControlCommand : Command
    {
        public override string Description()
        {
            return "Control another player entity. Usage: control [GameObject name]";
        }

        public override void Execute(List<string> args)
        {
            if (args.Count != 0)
            {
                var gObject = GameObject.Find(args[0]);
                if (gObject)
                {
                    var playerComp = gObject.GetComponent<PlayerController>();
                    if (playerComp)
                        GameController.instance.SetPlayer(playerComp);
                }
            }
        }

        public override string Name()
        {
            return "control";
        }
    }
}