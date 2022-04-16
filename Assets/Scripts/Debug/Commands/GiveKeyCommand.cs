using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class GiveKeyCommand : Command
    {
        public override string Description()
        {
            return "Give yourself a Castle Key.";
        }

        public override void Execute(List<string> args)
        {
            if (GameController.instance.playerController)
            {
                var obj = GameObject.Instantiate(DebugController.instance.castleKeyPrefab);
                obj.transform.position = GameController.instance.player.transform.position;
            }
        }

        public override string Name()
        {
            return "key";
        }
    }
}