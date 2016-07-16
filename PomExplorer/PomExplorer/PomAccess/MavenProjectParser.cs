using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PomExplorer.PomAccess
{
    public class MavenProjectParser
    {

        private class NamespaceIgnorantXmlTextReader : XmlTextReader
        {
            public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }

            public override string NamespaceURI
            {
                get { return ""; }
            }
        }

        public MavenProject Parse(String xml)
        {
            var serializer = new XmlSerializer(typeof(MavenProject));
            var reader = new StringReader(xml);
            var o = serializer.Deserialize(new NamespaceIgnorantXmlTextReader(reader));

            return o as MavenProject;
        }
    }
}
