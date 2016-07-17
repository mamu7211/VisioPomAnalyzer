using PomExplorer.PomObjectModel;
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
        
        public MavenProject Parse(String fileName)
        {
            var serializer = new XmlSerializer(typeof(MavenProject));
            var reader = new StringReader(new StreamReader(fileName).ReadToEnd());
            var o = serializer.Deserialize(new NamespaceIgnorantXmlTextReader(reader));
            var project = o as MavenProject;

            if (project != null)
            {
                project.BaseDirectory = Path.GetDirectoryName(fileName);

                foreach(var moduleName in project.ModuleNames)
                {
                    var module = new Module(project, moduleName);
                    module.Project = Parse(module.ModuleFileName);
                    project.Modules.Add(module);
                    project.updateMissingAttributes(module);
                }
            }

            return project;
        }
    }
}
