using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomExplorer.Model;
using PomExplorer.Maven;
using System.IO;

namespace PomExplorerTests.ModelTest
{
    [TestClass]
    public class XmlProjectObjectModelTest
    {
        private String _fileNameParent = "../../SampleFiles/pom.xml";
        private String _fileNameModuleA = "../../SampleFiles/module-a/pom.xml";
        private String _fileNameModuleB = "../../SampleFiles/module-b/pom.xml";

        XmlProjectObjectModel pom;

        [TestInitialize]
        public void InitializeTest()
        {
            pom = XmlProjectObjectModel.from(_fileNameParent);
        }

        [TestMethod]
        public void TestParsingThrowsNoException()
        {
            Assert.IsNotNull(pom);
        }

        [TestMethod]
        public void TestParsingSetsFileInfo()
        {
            var pom = XmlProjectObjectModel.from(_fileNameParent);
            var expected = new FileInfo(_fileNameParent);
            Assert.AreEqual(expected.FullName, pom.FileInfo.FullName);
        }

        [TestMethod]
        public void TestParsingCreatesArtifact()
        {
            Assert.AreEqual("parent-artifactid", pom.ArtifactId);
            Assert.AreEqual("com.example.app", pom.GroupId);
            Assert.AreEqual("parent-name", pom.Name);
            Assert.AreEqual("parent-description", pom.Description);
            Assert.AreEqual("1.0.RELEASE", pom.Version);
        }

        [TestMethod]
        public void TestSubModuleNamesDeserialized()
        {
            Assert.AreEqual(2, pom.ModuleNames.Count);
        }

        [TestMethod]
        public void TestSubModulesCreated()
        {
            Assert.AreEqual(2, pom.Modules.Count);
        }

        [TestMethod]
        public void TestSubModuleMissingArtifactElementsAreInferredByParent()
        {
            Assert.AreEqual("com.example.app", pom.Modules[0].GroupId);
            Assert.AreEqual("1.0.RELEASE", pom.Modules[0].Version);
        }

        [TestMethod]
        public void TestModulesContainArtifact()
        {
            Assert.AreEqual("module-a-artifact", pom.Modules[0].ArtifactId);
            Assert.AreEqual("com.example.app", pom.Modules[0].GroupId);
            Assert.AreEqual("module-a-name", pom.Modules[0].Name);
            Assert.AreEqual("module-a-description", pom.Modules[0].Description);
            Assert.AreEqual("1.0.RELEASE", pom.Modules[0].Version);
        }

    }
}
