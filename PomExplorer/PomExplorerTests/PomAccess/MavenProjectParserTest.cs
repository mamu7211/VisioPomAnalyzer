using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using PomExplorer.PomAccess;
using Moq;

namespace PomExplorerTests
{
    [TestClass]
    public class MavenProjectParserTest
    {
        MavenProjectParser parser = new MavenProjectParser();

        private String _fileNameParent = "../../SampleFiles/pom.xml";
        private String _fileNameModuleA = "../../SampleFiles/module-a/pom.xml";
        private String _fileNameModuleB = "../../SampleFiles/module-b/pom.xml";

        [TestMethod]
        public void TestParsingThrowsNoException()
        {
            var project = parser.Parse(_fileNameParent);
        }

        [TestMethod]
        public void TestProjectHasGroupAndArtifact()
        {
            var project = parser.Parse(_fileNameParent);

            Assert.AreEqual("com.example.parent", project.GroupId);
            Assert.AreEqual("app-artifact", project.ArtifactId);
            Assert.AreEqual("Parent pom.xml", project.Description);
            Assert.AreEqual("parent-pom", project.Name);
        }

        [TestMethod]
        public void TestDependencies()
        {
            var project = parser.Parse(_fileNameModuleA);
            Assert.AreEqual(3, project.Dependencies.Count);
        }

        [TestMethod]
        public void TestPartentHasModules()
        {
            var projectParent = parser.Parse(_fileNameParent);

            Assert.AreEqual("parent-pom", projectParent.Name);
            Assert.AreEqual(2, projectParent.Modules.Count);
        }

        [TestMethod]
        public void TestModulesHaveProjects()
        {
            var projectParent = parser.Parse(_fileNameParent);

            Assert.IsNotNull(projectParent.Modules[0].Project);
            Assert.IsNotNull(projectParent.Modules[0].Project);            
        }

        [TestMethod]
        public void TestModulesHaveValidArtifact()
        {
            var projectParent = parser.Parse(_fileNameParent);
            var projectChildA = projectParent.Modules[0].Project;
            var projectChildB = projectParent.Modules[1].Project;

            Assert.AreEqual("parent-pom", projectParent.Name);
            Assert.AreEqual("module-a", projectChildA.Name);
            Assert.AreEqual("module-b", projectChildB.Name);
        }

        [TestMethod]
        public void TestChildPomHasParentPomGroup()
        {
            var project = parser.Parse(_fileNameParent);
            var projectChild = project.Modules[0].Project;
            Assert.AreEqual(project.GroupId, projectChild.GroupId);
        }
    }
}
