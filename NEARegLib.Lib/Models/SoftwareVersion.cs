using System;
using System.Linq;
using System.Reflection;

namespace NEARegLib.Models
{
    public class SoftwareVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }

        public static SoftwareVersion GetCurrent()
        {
            string[] wantedFields = { "MajorMinorPatch", "InformationalVersion" };
            System.Type gitVersionInformationType;
            try
            {
                gitVersionInformationType = Assembly.GetEntryAssembly().GetType("GitVersionInformation");
            }
            catch
            {
                throw new Exception("Could not retrieve git version information. Be sure that the nuget package GitVersion.MsBuild is included in the entry assembly.");
            }
            var fields = gitVersionInformationType.GetFields();
            var informationalVersion = fields.Where(f => f.Name.Equals("InformationalVersion")).FirstOrDefault().GetValue(null);

            Assembly a = Assembly.GetEntryAssembly();

            return new SoftwareVersion { Name = a.FullName.Split(',').First(), Version = informationalVersion.ToString() };
        }
    }
}
