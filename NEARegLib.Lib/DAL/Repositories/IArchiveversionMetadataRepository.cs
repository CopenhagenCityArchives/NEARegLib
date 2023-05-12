using neaweb.Models;
using System.Threading.Tasks;

namespace neaweb_dapper.DAL.Repositories
{
    public interface IArchiveversionMetadataRepository : IGenericRepository<ArchiveversionMetadata>
    {
        public Task<ArchiveversionMetadata> GetArchiveversionMetadataByIdentifier(string archiveversionNumber);
    }
}