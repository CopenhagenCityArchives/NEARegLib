using NEARegLib.Models;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public interface IArchiveversionMetadataRepository : IGenericRepository<ArchiveversionMetadata>
    {
        public Task<ArchiveversionMetadata> GetArchiveversionMetadataByIdentifier(string archiveversionNumber);
    }
}