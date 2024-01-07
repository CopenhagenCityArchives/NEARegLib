using NEARegLib.Models;

namespace NEARegLib.DAL.Repositories
{
    public interface IArchiveversionMetadataRepository : IGenericRepository<ArchiveversionMetadata>
    {
        public ArchiveversionMetadata GetArchiveversionMetadataByIdentifier(string archiveversionNumber);
    }
}