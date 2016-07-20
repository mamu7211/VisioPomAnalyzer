using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Visio = Microsoft.Office.Interop.Visio;

namespace PomExplorer.Maven
{
    [Serializable, XmlRoot("project")]
    public class XmlProjectObjectModel : XmlArtifact
    {
        [XmlIgnore]
        public FileInfo FileInfo { get; private set; }

        public static XmlProjectObjectModel from(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            var projectSerializer = new XmlSerializer(typeof(XmlProjectObjectModel));
            var reader = new StringReader(new StreamReader(fileName).ReadToEnd());
            var result = projectSerializer.Deserialize(new NamespaceIgnorantXmlTextReader(reader)) as XmlProjectObjectModel;

            if (result != null)
            {
                result.FileInfo = fileInfo;
            }

            return result;
        }

        private class NamespaceIgnorantXmlTextReader : XmlTextReader
        {
            public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }
            public override string NamespaceURI
            {
                get { return ""; }
            }
        }

        [XmlElement("parent")]
        public XmlArtifact Parent;

        [XmlElement("name")]
        public string Name;

        [XmlElement("description")]
        public string Description;

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<XmlDependency> Dependencies = new List<XmlDependency>();

        [XmlArray("modules")]
        [XmlArrayItem("module")]
        public List<string> Modules = new List<string>();

        [XmlIgnore]
        public Visio.Shape Shape;
    }
}

