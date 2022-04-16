using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class DeleteCommand : Command
    {
        public override string Description()
        {
            return "Deletes a GameObject by name. Usage: delete [GameObject name]";
        }

        public override void Execute(List<string> args)
        {
            if (args.Count == 0)
            {
                DebugController.PrintErrorLine("Specify a gameobject name.");
                return;
            }
            var gm = GameObject.Find(args[0]);
            if (gm)
            {
                GameObject.Destroy(gm);
            }
            else
            {
                DebugController.PrintErrorLine("Couldn't find GameObject with name " + args[0] + ".");
                return;
            }
        }

        public override string Name()
        {
            return "delete";
        }
    }
}