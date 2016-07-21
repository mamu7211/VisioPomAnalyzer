﻿using PomExplorer.PomObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Visio = Microsoft.Office.Interop.Visio;

namespace PomExplorer.Maven
{
    [Serializable, XmlRoot("project")]
    public class XmlPom : XmlArtifact
    {

        #region XML Serializable Attributes

        [XmlElement("parent")]
        public XmlArtifact Parent;

        [XmlElement("name")]
        public string Name;

        [XmlElement("description")]
        public string Description;

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<XmlDependency> Dependencies = new List<XmlDependency>();

        [XmlArray("modules")]
        [XmlArrayItem("module")]
        public List<string> ModuleNames = new List<string>();

        #endregion

        [XmlIgnore]
        public FileInfo FileInfo { get; private set; }

        private List<XmlPom> _modules = new List<XmlPom>();

        [XmlIgnore]
        public ReadOnlyCollection<XmlPom> Modules
        {
            get { return _modules.AsReadOnly(); }
        }

        public static XmlPom from(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            var result = parseFile(fileInfo);

            if (result != null)
            {
                updateNonXmlAttributes(fileInfo, result);
            }

            return result;
        }

        private static XmlPom parseFile(FileInfo fileInfo)
        {
            var projectSerializer = new XmlSerializer(typeof(XmlPom));
            var reader = new StringReader(new StreamReader(fileInfo.FullName).ReadToEnd());
            return projectSerializer.Deserialize(new NamespaceIgnorantXmlTextReader(reader)) as XmlPom;
        }

        private static void updateNonXmlAttributes(FileInfo fileInfo, XmlPom result)
        {
            result.FileInfo = fileInfo;

            foreach (var moduleName in result.ModuleNames)
            {
                var moduleFileInfo = new FileInfo(Path.Combine(fileInfo.DirectoryName, moduleName, "pom.xml"));
                var subModule = from(moduleFileInfo.FullName);

                result._modules.Add(subModule);
       
                if (String.IsNullOrEmpty(subModule.Version)) subModule.Version = result.Version;
                if (String.IsNullOrEmpty(subModule.GroupId)) subModule.GroupId = result.GroupId;
            }
        }

        private class NamespaceIgnorantXmlTextReader : XmlTextReader
        {
            public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }
            public override string NamespaceURI
            {
                get { return ""; }
            }
        }


    }
}
