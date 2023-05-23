using Moq;
using NEARegLib.Models;
using NEARegLib.DAL.Repositories;
using NEARegLib.DAL.UnitsOfWork;
using NUnit.Framework;
using NEARegLib.DAL.UnitOfWork;
using System.Numerics;

namespace NEARegLib.Test
{
    public class NeaRegTests
    {
        private Mock<IArchiveversionMetadataRepository> _archiveversionMetadataRepository;
        private Mock<ISoftwareVersionRepository> _softwareVersionRepository;
        private Mock<ILocationRepository> _locationRepository;
        private Mock<IGenericRepository<LogEntry>> _logEntryRepository;
        private Mock<IUnitOfWork> _unitOfWorkRepository;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkRepository = new Mock<IUnitOfWork>();
            _archiveversionMetadataRepository = new Mock<IArchiveversionMetadataRepository>();
            _softwareVersionRepository = new Mock<ISoftwareVersionRepository>();
            _locationRepository = new Mock<ILocationRepository>();
            _logEntryRepository = new Mock<IGenericRepository<LogEntry>>();
        }

        [Test]
        public Task UpdateArchiveversionAddLogEntry_ArchiveversionIdIsNull_ThrowException()
        {
            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var avEntry = new ArchiveversionMetadata();

            var result = neaReg.UpdateArchiveversionAddLogEntry(avEntry, LogEntryType.MiNEA);

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_SoftwareVersionRepositoryThrowsException_ThrowException()
        {
            _softwareVersionRepository.Setup(s => s.InsertOrGetSoftwareVersionIdByNameAndVersion(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var avEntry = new ArchiveversionMetadata()
            {
                Id = 1
            };

            var result = neaReg.UpdateArchiveversionAddLogEntry(avEntry, LogEntryType.MiNEA);

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddLogEntryUpdateArchiveversionMetadata_ValidInputDataSaved_StartAndCommitTransactionReturnTrue()
        {
            _softwareVersionRepository.Setup(s => s.InsertOrGetSoftwareVersionIdByNameAndVersion(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new SoftwareVersion() { Id = 1 });

            //Return existing archiveversion
            var existingArchiveversion = new ArchiveversionMetadata() { Id = 1 };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingArchiveversion);
            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var av = new ArchiveversionMetadata()
            {
                Id = 1
            };

            var result = await neaReg.UpdateArchiveversionAddLogEntry(av, LogEntryType.MiNEA);

            _unitOfWorkRepository.Verify(u => u.StartTransaction(), Times.Once(), "Must start transaction");
            _unitOfWorkRepository.Verify(u => u.Commit(), Times.Once(), "Must commit transaction");

            Assert.That(result, Is.True);
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_NonExistingArchiveversion_ThrowException()
        {
            // Return existing archiveversion
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(() => null);

            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var updatedAv = new ArchiveversionMetadata() { Id = 1 };

            var result = neaReg.UpdateArchiveversionAddLogEntry(updatedAv,LogEntryType.MiNEA);

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddLogEntryUpdateArchiveversionMetadata_ExistingArchiveversionHasChanged_UpdateArchiveversion()
        {
            _softwareVersionRepository.Setup(s => s.InsertOrGetSoftwareVersionIdByNameAndVersion(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new SoftwareVersion() { Id = 1 });
            
            // Return existing archiveversion
            var existingAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.3f };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingAv);

            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var changedAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 10.0f };

            Assert.That(await neaReg.UpdateArchiveversionAddLogEntry(changedAv, LogEntryType.MiNEA), Is.True);

            _archiveversionMetadataRepository.Verify(a => a.Update(changedAv), Times.Once, "Must update archiveversion if it exists");
        }

        [Test]
        public async Task AddLogEntryUpdateArchiveversionMetadata_ExistingArchiveversionHasntChanged_DontUpdateArchiveversion()
        {
            _softwareVersionRepository.Setup(s => s.InsertOrGetSoftwareVersionIdByNameAndVersion(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new SoftwareVersion() { Id = 1 });

            // Return existing archiveversion
            var existingAv = new ArchiveversionMetadata();
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ReturnsAsync(existingAv);

            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            Assert.That(await neaReg.UpdateArchiveversionAddLogEntry(existingAv, LogEntryType.MiNEA), Is.True);

            _archiveversionMetadataRepository.Verify(a => a.Update(It.IsAny<ArchiveversionMetadata>()), Times.Never, "Must not update archiveversion if it hasn't changed");
        }

        [Test]
        public Task AddLogEntryUpdateArchiveversionMetadata_ErrorOnRetriveArchiveversion_ThrowException()
        {
            // Return existing archiveversion and throw exception when updating it
            var existingAv = new ArchiveversionMetadata() { Id = 1 };
            _archiveversionMetadataRepository.Setup(a => a.Retrieve(It.IsAny<int>())).ThrowsAsync(new Exception());

            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var result = neaReg.UpdateArchiveversionAddLogEntry(existingAv, LogEntryType.MiNEA);

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

            var neaReg = new NeaReg(_unitOfWorkRepository.Object, _archiveversionMetadataRepository.Object, _softwareVersionRepository.Object, _locationRepository.Object, _logEntryRepository.Object);

            var updatedAv = new ArchiveversionMetadata() { Id = 1, TotalSize = 1.2f };

            var result = neaReg.UpdateArchiveversionAddLogEntry(existingAv, LogEntryType.MiNEA);

            _unitOfWorkRepository.Verify(u => u.RollBack(), Times.Once, "Must call rollback on update error");

            Assert.That(result.Exception?.InnerException is Exception, Is.True);
            return Task.CompletedTask;
        }
    }
}