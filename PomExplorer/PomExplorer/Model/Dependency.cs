using PomExplorer.Maven;
using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorer.Model
{
    public class Dependency : Artifact
    {
        public static Dependency From(XmlDependency xmlDependency)
        {
            var dependency = Artifact.From(xmlDependency).As<Dependency>();

            dependency.Scope = xmlDependency.Scope ?? DefaultValue;

            return dependency;
        }

        public string Scope { get; private set; }
    }
}
