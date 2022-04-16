using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class RenameCommand : Command
    {
        public override string Description()
        {
            return "Rename a GameObject by name. Usage: rename [GameObject name] [New name]";
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
                if (args.Count > 1)
                    gm.name = args[1];
            }
        }

        public override string Name()
        {
            return "rename";
        }
    }
}