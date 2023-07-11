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
            var gitVersionInformationType = Assembly.GetExecutingAssembly().GetType("GitVersionInformation");
            var fields = gitVersionInformationType.GetFields();
            var informationalVersion = fields.Where(f => f.Name.Equals("InformationalVersion")).FirstOrDefault().GetValue(null);

            Assembly a = Assembly.GetEntryAssembly();

            return new SoftwareVersion { Name = a.FullName.Split(',').First(), Version = informationalVersion.ToString() };
        }
    }
}
