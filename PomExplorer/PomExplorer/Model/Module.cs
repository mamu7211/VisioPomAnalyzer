using PomExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorer.PomObjectModel
{
    public class Module
    {
        public string Identifier { get; protected set; }
        public List<Module> Modules { get; private set; }
        public List<Dependency> Dependencies { get; private set; }

        public Module(string identifier, List<Module> modules, List<Dependency> dependencies)
        {
            Identifier = identifier ?? "<no-identifier>";
            Modules = modules ?? new List<Module>();
            Dependencies = dependencies ?? new List<Dependency>();
        }
    }
}
