using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class GetGameObjectsCommand : Command
    {
        public override string Description()
        {
            return "Get all GameObjects. Usage: getgameobjects [root](optional, if 1 will only display gameobjects without parents)";
        }

        public override void Execute(List<string> args)
        {
            var rootOnly = false;
            if (args.Count > 0)
            {
                if (args[0] == "1")
                    rootOnly = true;
            }
            var allobjs = GameObject.FindObjectsOfType<GameObject>();
            if (rootOnly)
            {
                DebugController.PrintLine("[GameObjects in current scene]");
                foreach (var element in allobjs)
                {
                    if (element.scene.isLoaded && element.transform.parent == null)
                        DebugController.PrintLine(element.name);
                }
            }
            else
            {
                DebugController.PrintLine("[GameObjects in current scene]");
                foreach (var element in allobjs)
                {
                    if (element.scene.isLoaded)
                        DebugController.PrintLine(element.name);
                }
                DebugController.PrintLine("[GameObjects not in current scene]");
                foreach (var element in allobjs)
                {
                    if (!element.scene.isLoaded)
                        DebugController.PrintLine(element.name);
                }
            }
        }

        public override string Name()
        {
            return "getgameobjects";
        }
    }
}