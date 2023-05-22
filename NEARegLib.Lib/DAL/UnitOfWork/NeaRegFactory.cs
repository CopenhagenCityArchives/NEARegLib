using NEARegLib.DAL;
using NEARegLib.DAL.Repositories;
using NEARegLib.DAL.UnitsOfWork;
using System;

namespace neaweb_dapper.DAL.UnitOfWork
{
    public class NeaRegFactory
    {
        private readonly string _connectionString;
        public NeaRegFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string must be set");
            }

            _connectionString = connectionString;
        }
        public NeaReg GetNew()
        {
            // Get connection factory and connection string
            ConnectionFactory conFac = new ConnectionFactory(_connectionString);
            var connection = conFac.GetConnection;

            // Get unit of work and repositories
            var unitOfWork = new NEARegLib.DAL.UnitsOfWork.UnitOfWork(connection);
            var archiveversionMetadataRepository = new ArchiveversionMetadataRepository(unitOfWork);
            var logEntryMetadataRepository = new LogEntryRepository(unitOfWork);
            var softwareVersionRepository = new SoftwareVersionRepository(unitOfWork);
            var locationRepository = new LocationRepository(unitOfWork);

            return new NeaReg(unitOfWork, archiveversionMetadataRepository, softwareVersionRepository, locationRepository, logEntryMetadataRepository);
        }
    }
}
