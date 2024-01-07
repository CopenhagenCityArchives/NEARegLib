using NEARegLib.Models;

namespace NEARegLib.DAL.Repositories
{
    public interface ISoftwareVersionRepository : IGenericRepository<SoftwareVersion>
    {
        public SoftwareVersion InsertOrGetSoftwareVersionIdByNameAndVersion(string name, string version);
    }
}