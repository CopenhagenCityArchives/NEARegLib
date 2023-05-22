using NEARegLib.Models;
using NEARegLib.DAL.UnitsOfWork;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public class ArchiveversionMetadataRepository : GenericRepository<ArchiveversionMetadata>, IArchiveversionMetadataRepository
    {
        protected override string GetAllStatement
        {
            get
            {
                return "SELECT ID as Id, IF(LENGTH(arkiv_nr) = 5, CONCAT('000',arkiv_nr), arkiv_nr) as Number, av_stoerrelse as TotalSize, av_filmaengde as FilesCount, IF(minea_genrejst = 'n', 0, 1) as Searchable, minea_soeg as SearchesCount, filarkiv as LocationId, antal_zip_pakker as ZipPackagesCount FROM arkiveringsversion WHERE 1";
            }
        }

        protected override string GetStatement
        {
            get
            {
                return "SELECT ID as Id, IF(LENGTH(arkiv_nr) = 5, CONCAT('000',arkiv_nr), arkiv_nr) as Number, av_stoerrelse as TotalSize, av_filmaengde as FilesCount, IF(minea_genrejst = 'n', 0, 1) as Searchable, minea_soeg as SearchesCount, filarkiv as LocationId, antal_zip_pakker as ZipPackagesCount FROM arkiveringsversion WHERE ID = @Id";
            }
        }

        protected override string InsertStatement
        {
            get
            {
                throw new NotImplementedException("Unsupported: Cannot insert archiveversions");
            }
        }

        protected override string UpdateStatement
        {
            get
            {
                return "UPDATE arkiveringsversion SET av_stoerrelse = @TotalSize, av_filmaengde = @FilesCount, minea_genrejst = IF(@Searchable = 0, 'n', 'y'), minea_soeg = @SearchesCount, filarkiv = @LocationId, antal_zip_pakker = @ZipPackagesCount WHERE ID = @Id LIMIT 1";
            }
        }

        public ArchiveversionMetadataRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        /// <summary>
        /// Get an archiveversion based on its identifier (archiveversion number). An exception is thrown if more than 1 archiveversion metadata is found
        /// </summary>
        /// <param name="archiveversionNumber"></param>
        /// <returns>A Task that returns an ArchiveversionMetadata object if it exists otherwise null</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ArchiveversionMetadata> GetArchiveversionMetadataByIdentifier(string archiveversionNumber)
        {
            var avs = await RetrieveAll();

            var filteredAvs = avs.Where(av => av.Number.ToLower().Equals(archiveversionNumber.ToLower())).ToList();

            if (filteredAvs.Count > 1)
            {
                throw new ArgumentException("More than one matching archiveversion was found. This should not be possible.");
            }

            return filteredAvs.FirstOrDefault();
        }
    }
}
