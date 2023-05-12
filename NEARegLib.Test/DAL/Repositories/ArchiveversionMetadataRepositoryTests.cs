using Moq;
using neaweb.Models;
using neaweb_dapper.DAL.Repositories;
using neaweb_dapper.DAL.UnitsOfWork;
using NUnit.Framework;

namespace neaweb.Test.DAL.Repositories
{
    public class ArchiveversionMetadataRepositoryTests
    {
        private Mock<ArchiveversionMetadataRepository> avRep;
        private List<ArchiveversionMetadata> avList;

        [SetUp]
        public void SetUp()
        {
            avRep = new Mock<ArchiveversionMetadataRepository>(new Mock<IUnitOfWork>().Object);

            avList = new List<ArchiveversionMetadata>()
            {
                new ArchiveversionMetadata() { Id = 1, Number = "AVID.KSA.1"},
                new ArchiveversionMetadata() {Id = 2, Number = "AVID.KSA.2"}
            };
        }
        [Test]
        public async Task GetArchiveversionMetadataByIdentifier_ExistingArchiveversion_ReturnArchiveversion()
        {
            avRep.Setup(a => a.RetrieveAll()).ReturnsAsync(avList);

            var result = await avRep.Object.GetArchiveversionMetadataByIdentifier("avid.ksa.2");

            Assert.That(avList[1], Is.EqualTo(result));
        }

        [Test]
        public async Task GetArchiveversionMetadataByIdentifier_NonExistingArchiveversion_ReturnNull()
        {
            avRep.Setup(a => a.RetrieveAll()).ReturnsAsync(avList);

            var result = await avRep.Object.GetArchiveversionMetadataByIdentifier("does_not_exist");

            Assert.That(result, Is.Null);
        }

        [Test]
        public Task GetArchiveversionMetadataByIdentifier_DuplicateValue_ThrowException()
        {
            avList = new List<ArchiveversionMetadata>()
            {
                new ArchiveversionMetadata() { Id = 1, Number = "AVID.KSA.1"},
                new ArchiveversionMetadata() {Id = 2, Number = "AVID.KSA.1"}
            };

            avRep.Setup(a => a.RetrieveAll()).ReturnsAsync(avList);

            var result = avRep.Object.GetArchiveversionMetadataByIdentifier("AVID.KSA.1");

            Assert.That(result.Exception?.InnerException is ArgumentException, Is.True);
            return Task.CompletedTask;
        }
    }
}
