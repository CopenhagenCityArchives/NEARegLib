using NEARegLib.Models;
using NEARegLib.DAL.UnitsOfWork;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public class SoftwareVersionRepository : GenericRepository<SoftwareVersion>, ISoftwareVersionRepository
    {
        protected override string GetAllStatement
        {
            get
            {
                return "SELECT * FROM software_version WHERE 1;";
            }
        }

        protected override string GetStatement
        {
            get
            {
                return "SELECT * FROM software_version WHERE id = @Id;";
            }
        }

        protected override string InsertStatement
        {
            get
            {
                return "INSERT INTO software_version (name, version) VALUES (@Name, @Version);";
            }
        }

        protected override string UpdateStatement
        {
            get
            {
                throw new NotImplementedException("Unsupported: Cannot update software versions");
            }
        }

        public SoftwareVersionRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Returns a SoftwareVersion, inserting it if it does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<SoftwareVersion> InsertAndGetSoftwareVersionByNameAndVersion(string name, string version)
        {
            var allSoftwareVersions = await RetrieveAll();

            var result = allSoftwareVersions.Where(sv => sv.Name.ToLower().Equals(name.ToLower()) && sv.Version.ToLower().Equals(version.ToLower())).FirstOrDefault();

            result ??= await Create(new SoftwareVersion() { Name = name, Version = version });

            return result;
        }
    }
}
