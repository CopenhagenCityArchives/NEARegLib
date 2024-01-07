using NEARegLib.DAL.UnitsOfWork;
using NEARegLib.Models;
using System;
using System.Data;
using System.Linq;

namespace NEARegLib.DAL.Repositories
{
    public class SoftwareVersionRepository : GenericRepository<SoftwareVersion>, ISoftwareVersionRepository
    {
        protected override string GetAllStatement
        {
            get
            {
                return "SELECT id, program as Name, informationalVersion as version, created FROM software_version WHERE 1;";
            }
        }

        protected override string GetStatement
        {
            get
            {
                return "SELECT id, program as Name, informationalVersion as version, created FROM software_version WHERE id = @Id;";
            }
        }

        protected override string InsertStatement
        {
            get
            {
                return "INSERT INTO software_version (program, informationalVersion) VALUES (@Name, @Version);";
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
        public SoftwareVersion InsertOrGetSoftwareVersionIdByNameAndVersion(string name, string version)
        {
            var allSoftwareVersions = RetrieveAll();

            var result = allSoftwareVersions.Where(sv => sv.Name.ToLower().Equals(name.ToLower()) && sv.Version.ToLower().Equals(version.ToLower())).FirstOrDefault();

            result ??= Create(new SoftwareVersion() { Name = name, Version = version });

            return result;
        }
    }
}
