using Moq;
using NEARegLib.Models;
using NEARegLib.DAL.Repositories;
using NEARegLib.DAL.UnitsOfWork;
using NUnit.Framework;

namespace NEARegLib.Test.DAL.Repositories
{
    public class SoftwareVersionRepositoryTests
    {
        private Mock<SoftwareVersionRepository> svRep;
        private List<SoftwareVersion> softwareList;

        [SetUp]
        public void SetUp()
        {
            svRep = new Mock<SoftwareVersionRepository>(new Mock<IUnitOfWork>().Object);

            softwareList = new List<SoftwareVersion>()
            {
                new SoftwareVersion() { Id = 1, Name = "WolfPack", Version = "Version1+4581.54"},
                new SoftwareVersion() {Id = 2, Name = "WolfPack", Version = "2.1.8"}
            };
        }
        [Test]
        public async Task InsertAndGetSoftwareVersionByNameAndVersion_ExistingSoftwareVersion_ReturnExistingSoftwareVersion()
        {
            // Return list with software versions
            svRep.Setup(a => a.RetrieveAll()).ReturnsAsync(softwareList);

            var result = await svRep.Object.InsertAndGetSoftwareVersionByNameAndVersion("wolfpack", "version1+4581.54");

            Assert.That(softwareList[0], Is.EqualTo(result));
        }

        [Test]
        public async Task InsertAndGetSoftwareVersionByNameAndVersion_NonExistingSoftwareVersion_CreateAndReturnNewSoftwareVersion()
        {
            // Return empty list from "db"
            svRep.Setup(a => a.RetrieveAll()).ReturnsAsync(new List<SoftwareVersion>());
            svRep.Setup(sv => sv.Create(It.IsAny<SoftwareVersion>())).ReturnsAsync(new SoftwareVersion());

            var result = await svRep.Object.InsertAndGetSoftwareVersionByNameAndVersion("does_not_exist", "new_version");

            svRep.Verify(sv => sv.Create(It.IsAny<SoftwareVersion>()), Times.Once, "Must create new software version if it does not exist");
            Assert.That(result, Is.Not.Null);
        }
    }
}
