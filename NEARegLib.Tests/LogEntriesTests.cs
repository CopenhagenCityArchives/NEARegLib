using Moq;
using NEARegLib.Models;
using NEARegLib;
using NEARegLib.DAL.Repositories;
using NEARegLib.DAL.UnitsOfWork;
using NUnit.Framework;

namespace NEARegLib.Test
{
    public class LogEntriesTests
    {
        private Mock<IArchiveversionMetadataRepository> _archiveversionMetadataRepository;
        private Mock<ISoftwareVersionRepository> _softwareVersionRepository;
        private Mock<IGenericRepository<LogEntry>> _logEntryRepository;
        private Mock<IUnitOfWork> _unitOfWorkRepository;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkRepository = new Mock<IUnitOfWork>();
            _archiveversionMetadataRepository = new Mock<IArchiveversionMetadataRepository>();
            _softwareVersionRepository = new Mock<ISoftwareVersionRepository>();
            _logEntryRepository = new Mock<IGenericRepository<LogEntry>>();
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_ArchiveversionIsNull_ThrowException()
        {
            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = null,
                SoftwareVersion = new SoftwareVersion()
            };

            var result = avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry);

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_SoftwareVersionIsNull_ThrowExceptionReturn()
        {
            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = new ArchiveversionMetadata() { Id = 1 },
                SoftwareVersion = null
            };

            var result = avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry);

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddLogEntryUpdateArchiveversionMetadata_ValidInputDataSaved_StartAndCommitTransactionReturnTrue()
        {
            //Return existing archiveversion
            var existingArchiveversion = new ArchiveversionMetadata() { Id = 1 };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingArchiveversion);
            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = existingArchiveversion,
                SoftwareVersion = new SoftwareVersion()
            };

            var result = await avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry);

            _unitOfWorkRepository.Verify(u => u.StartTransaction(), Times.Once(), "Must start transaction");
            _unitOfWorkRepository.Verify(u => u.Commit(), Times.Once(), "Must commit transaction");

            Assert.That(result, Is.True);
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_NonExistingArchiveversion_ThrowException()
        {
            // Return existing archiveversion
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(() => null);

            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var updatedAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.2f };
            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = updatedAv,
                SoftwareVersion = new SoftwareVersion()
            };

            var result = avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry);

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddLogEntryUpdateArchiveversionMetadata_ExistingArchiveversionHasChanged_UpdateArchiveversion()
        {
            // Return existing archiveversion
            var existingAv = new ArchiveversionMetadata() { Id = 1 };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingAv);

            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var updatedAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.2f };
            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = updatedAv,
                SoftwareVersion = new SoftwareVersion()
            };

            Assert.That(await avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry), Is.True);

            _archiveversionMetadataRepository.Verify(a => a.Update(updatedAv), Times.Once, "Must update archiveversion if it exists");
        }

        [Test]
        public async Task AddLogEntryUpdateArchiveversionMetadata_ExistingArchiveversionHasntChanged_DontUpdateArchiveversion()
        {
            // Return existing archiveversion
            var existingAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.2f };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingAv);

            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var updatedAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.2f };
            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = updatedAv,
                SoftwareVersion = new SoftwareVersion()
            };

            Assert.That(await avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry), Is.True);

            _archiveversionMetadataRepository.Verify(a => a.Update(It.IsAny<ArchiveversionMetadata>()), Times.Never, "Must not update archiveversion if it hasn't changed");
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_ErrorOnRetriveArchiveversion_ThrowException()
        {
            // Return existing archiveversion and throw exception when updating it
            var existingAv = new ArchiveversionMetadata() { Id = 1 };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ThrowsAsync(new Exception());

            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = new ArchiveversionMetadata() { Id = 1 },
                SoftwareVersion = new SoftwareVersion()
            };

            var result = avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry);

            Assert.That(result.Exception?.InnerException is Exception, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_ErrorOnUpdateArchiveversion_RollBackTransactionThrowExceptione()
        {
            // Return existing archiveversion and throw exception when updating it
            var existingAv = new ArchiveversionMetadata() { Id = 1 };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingAv);
            _archiveversionMetadataRepository.Setup(a => a.Update(It.IsAny<ArchiveversionMetadata>())).ThrowsAsync(new Exception());

            var avEntryLog = new LogEntries(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _logEntryRepository.Object);

            var updatedAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.2f };
            var logEntry = new LogEntry()
            {
                ArchiveversionMetadata = updatedAv,
                SoftwareVersion = new SoftwareVersion()
            };

            var result = avEntryLog.AddLogEntryUpdateArchiveversionMetadata(logEntry);

            _unitOfWorkRepository.Verify(u => u.RollBack(), Times.Once, "Must call rollback on update error");

            Assert.That(result.Exception?.InnerException is Exception, Is.True);
            return Task.CompletedTask;
        }
    }
}