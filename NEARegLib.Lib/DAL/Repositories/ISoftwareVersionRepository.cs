using neaweb.Models;
using System.Threading.Tasks;

namespace neaweb_dapper.DAL.Repositories
{
    public interface ISoftwareVersionRepository : IGenericRepository<SoftwareVersion>
    {
        public Task<SoftwareVersion> InsertAndGetSoftwareVersionByNameAndVersion(string name, string version);
    }
}