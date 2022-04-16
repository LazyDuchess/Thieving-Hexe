using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class PostFXCommand : Command
    {
        public override string Description()
        {
            return "Toggle post processing.";
        }

        public override void Execute(List<string> args)
        {
            GamePostFX.instance.enabled = !GamePostFX.instance.enabled;
        }

        public override string Name()
        {
            return "postfx";
        }
    }
}