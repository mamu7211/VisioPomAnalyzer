using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using PomExplorer.PomAccess;

namespace PomExplorerTests
{
    [TestClass]
    public class UnitTest1
    {

        MavenProjectParser parser = new MavenProjectParser();

        String xml = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>"
            + "<project>"
            + "<groupId>Group-Id</groupId>"
            + "<artifactId>Artifact-Id</artifactId>"
            + "<version>0.0.1-SNAPSHOT</version>"
            + "<packaging>jar</packaging>"
            + "<name>Project Name</name>"
            + "<description>Project Description</description>"
            + "<dependencies>"
            + "		<dependency>"
            + "			<groupId>org.springframework.boot</groupId>"
            + "			<artifactId>spring-boot-starter-web</artifactId>"
            + "		</dependency>"
            + "		<dependency>"
            + "			<groupId>org.springframework.boot</groupId>"
            + "			<artifactId>spring-boot-starter-data-jpa</artifactId>"
            + "		</dependency>"
            + "		<dependency>"
            + "			<groupId>org.springframework.boot</groupId>"
            + "			<artifactId>spring-boot-starter-thymeleaf</artifactId>"
            + "		</dependency>"
            + "</dependencies>"
            + "</project>";

        [TestMethod]
        public void TestMethod1()
        {
            var project = parser.Parse(xml);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var project = parser.Parse(xml);

            Assert.AreEqual("Group-Id", project.GroupId);
            Assert.AreEqual("Artifact-Id", project.ArtifactId);
            Assert.AreEqual("Project Description", project.Description);
            Assert.AreEqual("Project Name", project.Name);
        }

        [TestMethod]
        public void TestDependencies()
        {
            var project = parser.Parse(xml);
            Assert.AreEqual(3, project.Dependencies.Count);
        }
    }
}
