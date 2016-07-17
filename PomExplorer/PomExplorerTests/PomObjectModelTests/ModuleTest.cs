using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomExplorer.PomObjectModel;
using System.IO;
using PomExplorer;

namespace PomExplorerTests.PomObjectModelTests
{
    [TestClass]
    public class ModuleTest
    {

        [TestMethod]
        public void TestModulParentBasePath()
        {
            MavenProject project = new MavenProject();
            project.BaseDirectory = "/base/directory/aggregator";
            
            Module module = new Module(project,"../module-a");

            String path = new FileInfo(@"\base\directory\module-a\pom.xml").FullName;
            Assert.AreEqual(path, module.ModuleFileName);
        }
    }
}
