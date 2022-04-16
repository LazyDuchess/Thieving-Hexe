using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public class CreatePlayerCommand : Command
    {
        public override string Description()
        {
            return "Instantiates a new player and runs all Default Items. Usage: createplayer [GameObject name (optional)]";
        }

        public override void Execute(List<string> args)
        {
            var newPlayer = GameObject.Instantiate(GameController.instance.playerPrefab);
            if (args.Count > 0)
            {
                newPlayer.name = args[0];
            }
            newPlayer.transform.position = GameController.instance.player.transform.position + (Vector3.right * 2f);
            
        }

        public override string Name()
        {
            return "createplayer";
        }
    }
}