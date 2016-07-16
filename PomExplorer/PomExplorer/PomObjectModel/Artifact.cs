using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PomExplorer
{
    public class Artifact
    {
        [XmlElement("groupId")]
        public String GroupId;

        [XmlElement("artifactId")]
        public String ArtifactId;

        [XmlElement("version")]
        public String Version;

        [XmlElement("packaging")]
        public String Packaging;

    }
}
