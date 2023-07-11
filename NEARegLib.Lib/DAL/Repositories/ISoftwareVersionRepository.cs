using NEARegLib.Models;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public interface ISoftwareVersionRepository : IGenericRepository<SoftwareVersion>
    {
        public SoftwareVersion InsertOrGetSoftwareVersionIdByNameAndVersion(string name, string version);
    }
}