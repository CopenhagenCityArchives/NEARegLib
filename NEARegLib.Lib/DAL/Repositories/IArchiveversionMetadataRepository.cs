using NEARegLib.Models;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public interface IArchiveversionMetadataRepository : IGenericRepository<ArchiveversionMetadata>
    {
        public ArchiveversionMetadata GetArchiveversionMetadataByIdentifier(string archiveversionNumber);
    }
}