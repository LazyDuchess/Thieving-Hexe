using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class SetTeamCommand : Command
    {
        public override string Description()
        {
            return "Sets the team of a health entity. Usage: setteam [GameObject name] [Team]";
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
                {
                    var hpController = gm.GetComponent<HealthController>();
                    if (hpController)
                    {
                        hpController.team = int.Parse(args[1]);
                    }
                }
            }
        }

        public override string Name()
        {
            return "setteam";
        }
    }
}