using PomExplorer.PomAccess;
using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Visio = Microsoft.Office.Interop.Visio;

namespace PomExplorer
{
    [Serializable, XmlRoot("project")]
    public class MavenProject : Artifact
    {
        [XmlElement("parent")]
        public Artifact Parent;

        [XmlElement("name")]
        public string Name;

        [XmlElement("description")]
        public string Description;

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<Dependency> Dependencies = new List<Dependency>();

        [XmlArray("modules")]
        [XmlArrayItem("module")]
        public List<string> ModuleNames = new List<string>();

        [XmlIgnore]
        public Visio.Shape Shape;

        [XmlIgnore]
        public List<Module> Modules = new List<Module>();

        [XmlIgnore]
        public string BaseDirectory { get; set; }

        internal void updateMissingAttributes(Module module)
        {
            var moduleProject = module.Project;
            if (String.IsNullOrEmpty(moduleProject.GroupId)) moduleProject.GroupId = this.GroupId;
            if (String.IsNullOrEmpty(moduleProject.Version)) moduleProject.Version = this.Version;
        }
    }
}

