using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{ 
    public class Console
    {
        public static bool initialized = false;

        public static Dictionary<string, Command> commandDictionary = new Dictionary<string, Command>();
        public static void Initialize()
        {
            if (initialized)
                return;
            initialized = true;
            AddCommand(new GodCommand());
            AddCommand(new HelpCommand());
            AddCommand(new GetGameObjectsCommand());
            AddCommand(new ClearCommand());
            AddCommand(new CloneCommand());
            AddCommand(new ControlCommand());
            AddCommand(new CreatePlayerCommand());
            AddCommand(new RenameCommand());
            AddCommand(new DeleteCommand());
            AddCommand(new SetTeamCommand());
            AddCommand(new GiveKeyCommand());
            AddCommand(new FirstPersonCommand());
            AddCommand(new PostFXCommand());
        }

        public static void AddCommand(Command command)
        {
            commandDictionary[command.Name()] = command;
        }

        static List<string> SplitArguments(string input)
        {
            var args = new List<string>();
            var trimmed = input.Trim();
            var last = 0;
            var instring = false;
            for(var i=0;i<trimmed.Length;i++)
            {
                if (trimmed[i] == '"')
                {
                    if (!instring)
                    {
                        last = i + 1;
                        instring = true;
                    }
                    else
                    {
                        args.Add(trimmed.Substring(last, i - last));
                        last = i + 1;
                        while (last < trimmed.Length && trimmed[last] == ' ')
                            last++;
                        i = last-1;
                        instring = false;
                    }
                }
                else
                {
                    if (trimmed[i] == ' ')
                    {
                        if (!instring)
                        {
                            args.Add(trimmed.Substring(last, i - last));
                            last = i + 1;
                            while (last < trimmed.Length && trimmed[last] == ' ')
                                last++;
                            i = last-1;
                        }
                    }
                }
            }
            args.Add(trimmed.Substring(last, trimmed.Length-last));
            return args;
        }

        public static void Execute(string input)
        {
            var args = SplitArguments(input);
            var cmd = args[0].ToLowerInvariant();
            args.RemoveAt(0);
            if (commandDictionary.ContainsKey(cmd))
                commandDictionary[cmd].Execute(args);
            else
                DebugController.PrintErrorLine("Can't find command " + cmd + ".");
        }
    }
}
