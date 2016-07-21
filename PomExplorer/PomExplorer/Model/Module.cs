using PomExplorer.Maven;
using PomExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorer.Model
{
    public class Module : Artifact
    {
        public static Module From(XmlPom pom) {
            var result = Artifact.From(pom).As<Module>();

            result.Name = pom.Name ?? Artifact.UnknownValue;
            result.Description = pom.Description ?? Artifact.UnknownValue;
            result.Packaging = pom.Packaging;
            
            return result;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Packaging { get; private set; }
    }
}
