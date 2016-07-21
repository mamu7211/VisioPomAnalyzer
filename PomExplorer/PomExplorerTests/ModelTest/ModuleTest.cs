using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomExplorer.PomObjectModel;
using System.IO;
using PomExplorer;
using PomExplorer.Maven;
using PomExplorer.Model;

namespace ModelTest.PomObjectModelTests
{
    [TestClass]
    public class ModuleTest
    {

        XmlPom xmlPom;

        [TestInitialize]
        public void TestInitialize()
        {
            xmlPom = new XmlPom();
            xmlPom.ArtifactId = "artifact-id";
            xmlPom.GroupId = "group-id";
            xmlPom.Packaging = "packaging";
            xmlPom.Version = "version";
            xmlPom.Name = "name";
            xmlPom.Description = "description";
        }

        [TestMethod]
        public void TestModulParentBasePath()
        {
            var module = Module.From(xmlPom);

            Assert.AreEqual("artifact-id", module.Id);
            Assert.AreEqual("group-id", module.GroupId);
            Assert.AreEqual("packaging", module.Packaging);
            Assert.AreEqual("version", module.Version);
            Assert.AreEqual("name", module.Name);
            Assert.AreEqual("description", module.Description);
        }
    }
}
