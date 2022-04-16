using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class HelpCommand : Command
    {
        public override string Description()
        {
            return "See all commands.";
        }

        public override void Execute(List<string> args)
        {
            foreach(var element in Console.commandDictionary)
            {
                DebugController.PrintLine(element.Key + " - " + element.Value.Description());
            }
        }

        public override string Name()
        {
            return "help";
        }
    }
}