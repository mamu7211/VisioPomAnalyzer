using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PomExplorer
{
    [Serializable, XmlRoot("project")]
    public class MavenProject : Artifact
    {
        [XmlElement("parent")]
        public Artifact Parent;

        [XmlElement("name")]
        public String Name;

        [XmlElement("description")]
        public String Description;

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<Dependency> Dependencies;
    }
}
