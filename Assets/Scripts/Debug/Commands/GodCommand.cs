using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class GodCommand : Command
    {
        public override string Description()
        {
            return "Toggle God mode.";
        }

        public override void Execute(List<string> args)
        {
            if (GameController.instance.playerController)
            {
                var player = GameController.instance.playerController;
                if (player.god)
                {
                    player.god = false;
                    DebugController.PrintLine("God mode disabled.");
                }
                else
                {
                    player.god = true;
                    DebugController.PrintLine("God mode enabled.");
                }
            }
        }

        public override string Name()
        {
            return "god";
        }
    }
}