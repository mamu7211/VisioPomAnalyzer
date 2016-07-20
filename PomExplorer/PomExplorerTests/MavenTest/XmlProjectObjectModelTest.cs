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
            Assert.AreEqual("app-artifact", pom.ArtifactId);
            Assert.AreEqual("com.example.app", pom.GroupId);
            Assert.AreEqual("parent-pom-name", pom.Name);
            Assert.AreEqual("parent-pom-description", pom.Description);
            Assert.AreEqual("1.0.RELEASE", pom.Version);
        }

    }
}
