using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PomExplorer.Maven
{
    public class XmlArtifact
    {
        [XmlElement("groupId")]
        public String GroupId;

        [XmlElement("artifactId")]
        public String ArtifactId;

        [XmlElement("version")]
        public String Version;

        [XmlElement("packaging")]
        public String Packaging;

        public String ArtifactSummary
        {
            get
            {
                return "group: " + GroupId + "\nartifact: " + ArtifactId + "\nversion: " + Version;
            }
        }

        public String ArtifactKey
        {
            get
            {
                return GroupId + ":" + ArtifactId + ":" + Version;
            }
        }
    }
}
