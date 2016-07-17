using PomExplorer.PomAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorer.PomObjectModel
{
    public class Module
    {

        private MavenProject _parent;
        
        public Module(MavenProject parentProject, String modulePath)
        {
            _parent = parentProject;
            ModulePath = modulePath;
        }

        public String ModulePath { get; private set; }

        public String ModuleFileName
        {
            get
            {
                return new FileInfo(Path.Combine(new string[] { _parent.BaseDirectory, ModulePath, "pom.xml" })).FullName;                
            }
        }

        public MavenProject Project { get; set; }
    }
}
