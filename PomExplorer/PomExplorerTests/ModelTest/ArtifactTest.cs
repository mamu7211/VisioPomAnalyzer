using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomExplorer.Maven;
using PomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorerTests.ModelTest
{
    [TestClass]
    public class ArtifactTest
    {
        XmlArtifact xmlArtifact;

        [TestInitialize]
        public void TestInitialize()
        {
            xmlArtifact = new XmlArtifact();
            xmlArtifact.ArtifactId = "artifact-id";
            xmlArtifact.GroupId = "group-id";
            xmlArtifact.Version = "version";
        }

        [TestMethod]
        public void TestArtifactFactory()
        {
            var artifact = Artifact.From(xmlArtifact);
            Assert.IsNotNull(artifact);
        }

        [TestMethod]
        public void TestArtifactValuesSet()
        {
            var artifact = Artifact.From(xmlArtifact);

            Assert.AreEqual("artifact-id", artifact.Id);
            Assert.AreEqual("group-id", artifact.GroupId);
            Assert.AreEqual("version", artifact.Version);
        }

        [TestMethod]
        public void TestArtifactValuesSetToDefault()
        {
            var artifact = Artifact.From(new XmlArtifact());

            Assert.AreEqual(Artifact.UnknownValue, artifact.Id);
            Assert.AreEqual(Artifact.UnknownValue, artifact.GroupId);
            Assert.AreEqual(Artifact.UnknownValue, artifact.Version);
        }
    }
}
