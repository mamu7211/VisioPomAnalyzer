using PomExplorer.Maven;
using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PomExplorer.Model
{
    public class Model
    {
        private Dictionary<String, XmlProjectObjectModel> _repository = new Dictionary<string, XmlProjectObjectModel>();

        private Module _parent;
        private Module Module {
            get {
                if (_parent == null)
                {
                    Build();
                }
                return _parent;
            }
        }



        private void Build()
        {

        }
        /*
            var file = new FileInfo(fileName);
            var projectSerializer = new XmlSerializer(typeof(XmlProjectObjectModel));            
            var reader = new StringReader(new StreamReader(fileName).ReadToEnd());
            var o = projectSerializer.Deserialize(new NamespaceIgnorantXmlTextReader(reader));

            var project = o as XmlProjectObjectModel;

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

                if (!_repository.ContainsKey(project.ArtifactKey))
                {
                    Trace.WriteLine("ADDING: " + project.ArtifactKey);
                    _repository.Add(project.ArtifactKey, project);                    
                }
                else {
                    Trace.WriteLine("FOUND: " + project.ArtifactKey);
                    project = _repository[project.ArtifactKey];
                }
            }

            return project;
        }*/
    }
}
