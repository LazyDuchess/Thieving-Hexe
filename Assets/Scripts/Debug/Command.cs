using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexe.Debug
{
    public abstract class Command
    {
        public virtual bool Show() { return true; }
        public abstract string Name();
        public abstract string Description();
        public abstract void Execute(List<string> args);
    }
}