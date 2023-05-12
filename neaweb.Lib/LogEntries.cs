using neaweb.Models;
using neaweb_dapper.DAL.Repositories;
using neaweb_dapper.DAL.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace neaweb_dapper
{
    /// <summary>
    /// This class handles creation of LogEntry objects and subsequent updates of related archiveversionmetadata
    /// </summary>
    public class LogEntries
    {
        public readonly IArchiveversionMetadataRepository ArchiveversionMetadataRepository;
        public readonly ISoftwareVersionRepository SoftwareVersionRepository;
        private readonly IGenericRepository<LogEntry> _logEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogEntries(IUnitOfWork unitOfWork, IArchiveversionMetadataRepository archiveversionMetadataRepository, ISoftwareVersionRepository softwareVersionRepository, IGenericRepository<LogEntry> logEntryRepository)
        {
            _unitOfWork = unitOfWork;
            ArchiveversionMetadataRepository = archiveversionMetadataRepository;
            SoftwareVersionRepository = softwareVersionRepository;
            _logEntryRepository = logEntryRepository;
        }

        /// <summary>
        /// Saves a LogEntry and updates the ArchiveversionMetadata object if it has changed
        /// </summary>
        /// <param name="LogEntry log"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> AddLogEntryUpdateArchiveversionMetadata(LogEntry log)
        {
            if (log.SoftwareVersion == null)
            {
                throw new ArgumentException("SoftwareVersion is required when inserting a LogEntry");
            }

            if (log.ArchiveversionMetadata == null)
            {
                throw new ArgumentException("ArchiveversionMetadata is required when inserting a LogEntry");
            }

            var existingAv = await ArchiveversionMetadataRepository.Retrieve(log.ArchiveversionMetadata.Id) ?? throw new ArgumentException("ArchiveversionMetadata must exist in database when inserting a LogEntry");

            _unitOfWork.StartTransaction();

            try
            {
                // Archiveversion already exists - update if it as changed
                if (!existingAv.Equals(log.ArchiveversionMetadata))
                {
                    await ArchiveversionMetadataRepository.Update(log.ArchiveversionMetadata);
                }

                await _logEntryRepository.Create(log);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();

                throw e;
            }

            _unitOfWork.Commit();

            return true;
        }
    }
}
