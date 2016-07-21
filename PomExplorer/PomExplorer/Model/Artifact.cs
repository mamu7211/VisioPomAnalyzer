using PomExplorer.Maven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomExplorer.Model
{
    public class Artifact
    {
        public const string UnknownValue = "<unknown>";
        public const string DefaultValue = "<default>";

        public static Artifact From(XmlArtifact xmlArtifact)
        {
            var result = new Artifact();
            result.Version = xmlArtifact.Version ?? UnknownValue;
            result.GroupId = xmlArtifact.GroupId ?? UnknownValue;
            result.Id = xmlArtifact.ArtifactId ?? UnknownValue;

            return result;
        }

        public T As<T>() where T : Artifact, new()
        {
            T artifact = new T();

            artifact.GroupId = this.GroupId;
            artifact.Version = this.Version;
            artifact.Id = this.Id;

            return artifact;
        }

        public string Id { get; private set; }
        public string GroupId { get; private set; }
        public string Version { get; private set; }
    }
}
