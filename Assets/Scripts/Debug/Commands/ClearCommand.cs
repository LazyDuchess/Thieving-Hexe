using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class ClearCommand : Command
    {
        public override string Description()
        {
            return "Clears the console.";
        }

        public override void Execute(List<string> args)
        {
            DebugController.Clear();
        }

        public override string Name()
        {
            return "clear";
        }
    }
}