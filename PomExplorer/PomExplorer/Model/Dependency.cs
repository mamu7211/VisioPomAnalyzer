using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorer.Model
{
    public class Dependency
    {
        public string Identifier { get; private set; }
        public Module Module { get; private set; }

        public Dependency(Module module, string identifier)
        {
            Module = module ?? new Module("",null,null);
            Identifier = identifier;
        }
    }
}
