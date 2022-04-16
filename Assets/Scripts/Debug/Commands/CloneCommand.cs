using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class CloneCommand : Command
    {
        public override string Description()
        {
            return "Clone a GameObject by name. Usage: clone [GameObject name] [(Optional)Name of clone]";
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
                var newO = GameObject.Instantiate(gm);
                if (args.Count > 1)
                    newO.name = args[1];
            }
            else
            {
                DebugController.PrintErrorLine("Couldn't find GameObject with name " + args[0] + ".");
                return;
            }
        }

        public override string Name()
        {
            return "clone";
        }
    }
}