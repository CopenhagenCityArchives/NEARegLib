using NEARegLib.Models;
using NEARegLib.DAL.Repositories;
using NEARegLib.DAL.UnitsOfWork;
using System;
using System.Threading.Tasks;

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
        private readonly IUnitOfWork _unitOfWork;

        public NEAReg(IUnitOfWork unitOfWork, IArchiveversionMetadataRepository archiveversionMetadataRepository, ISoftwareVersionRepository softwareVersionRepository, ILocationRepository locationRepository, IGenericRepository<LogEntry> logEntryRepository)
        {
            _unitOfWork = unitOfWork;
            ArchiveversionMetadataRepository = archiveversionMetadataRepository;
            _softwareVersionRepository = softwareVersionRepository;
            LocationRepository = locationRepository;
            _logEntryRepository = logEntryRepository;
        }

        public bool UpdateArchiveversionAddLogEntry(ArchiveversionMetadata av, LogEntryType type)
        {
            var existingAv = ArchiveversionMetadataRepository.Retrieve(av.Id) ?? throw new ArgumentException("ArchiveversionMetadata must exist in database when updating it");

            _unitOfWork.StartTransaction();

            try
            {
                // Archiveversion already exists - update if it as changed
                if (!existingAv.Equals(av))
                {
                    ArchiveversionMetadataRepository.Update(av);
                }

                AddLogEntry(av.Id, "Updated archiveversion metadata", type);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();

                throw e;
            }

            _unitOfWork.Commit();

            return true;
        }

        public LogEntry AddLogEntry(int archiveversionId, string description, LogEntryType logEntryType, bool errorsOccurred = false)
        {
            var currentSoftwareVersion = SoftwareVersion.GetCurrent();

            var softwareVersionId = _softwareVersionRepository.InsertOrGetSoftwareVersionIdByNameAndVersion(currentSoftwareVersion.Name, currentSoftwareVersion.Version).Id;

            var logEntry = new LogEntry()
            {
                SoftwareVersionId = softwareVersionId,
                Description = description,
                Type = logEntryType,
                ArchiveversionId = archiveversionId,
                ErrorsOccurred = errorsOccurred
            };

            return _logEntryRepository.Create(logEntry);
        }
    }
}
