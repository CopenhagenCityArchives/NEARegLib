using NEARegLib.DAL.Repositories;
using NEARegLib.DAL.UnitsOfWork;
using NEARegLib.Models;
using System;

namespace NEARegLib
{
    /// <summary>
    /// This class handles creation of LogEntry objects and subsequent updates of related archiveversionmetadata
    /// </summary>
    public class NEAReg
    {
        public readonly IArchiveversionMetadataRepository ArchiveversionMetadataRepository;
        private readonly ISoftwareVersionRepository _softwareVersionRepository;
        public readonly ILocationRepository LocationRepository;
        private readonly IGenericRepository<LogEntry> _logEntryRepository;
        public readonly IUnitOfWork UnitOfWork;

        public NEAReg(IUnitOfWork unitOfWork, IArchiveversionMetadataRepository archiveversionMetadataRepository, ISoftwareVersionRepository softwareVersionRepository, ILocationRepository locationRepository, IGenericRepository<LogEntry> logEntryRepository)
        {
            UnitOfWork = unitOfWork;
            ArchiveversionMetadataRepository = archiveversionMetadataRepository;
            _softwareVersionRepository = softwareVersionRepository;
            LocationRepository = locationRepository;
            _logEntryRepository = logEntryRepository;
        }

        public bool UpdateArchiveversionAddLogEntry(ArchiveversionMetadata av, SoftwareVersion softwareVersion, LogEntryType type)
        {
            var existingAv = ArchiveversionMetadataRepository.Retrieve(av.Id) ?? throw new ArgumentException("ArchiveversionMetadata must exist in database when updating it");

            try
            {
                // Archiveversion already exists - update if it as changed
                if (existingAv.Equals(av)) { return false; }

                ArchiveversionMetadataRepository.Update(av);

                LogEntry entry = new LogEntry()
                {
                    ArchiveversionId = av.Id,
                    Description = "Updated archiveversion metadata",
                    Type = type,
                    SoftwareVersionId = InsertOrGetSoftwareVersion(softwareVersion).Id
                };

                AddLogEntry(entry);

                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollBack();

                throw e;
            }
        }

        public SoftwareVersion InsertOrGetSoftwareVersion(SoftwareVersion softwareVersion)
        {
            return _softwareVersionRepository.InsertOrGetSoftwareVersionIdByNameAndVersion(softwareVersion.Name, softwareVersion.Version);
        }

        public LogEntry AddLogEntry(LogEntry entry)
        {
            if (entry.ArchiveversionId == 0)
            {
                throw new ArgumentException("ArchiveversionId must be set when adding log entries");
            }
            if (entry.SoftwareVersionId == 0)
            {
                throw new ArgumentException("Softwareversion must be set when adding log entries");
            }

            return _logEntryRepository.Create(entry);
        }
    }
}
