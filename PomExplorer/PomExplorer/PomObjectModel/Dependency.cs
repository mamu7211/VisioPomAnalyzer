using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PomExplorer.PomObjectModel
{
    [Serializable]
    public class Dependency : Artifact
    {
        [XmlElement("scope")]
        public String Scope;

        [XmlElement("type")]
        public String Type;
    }
}
