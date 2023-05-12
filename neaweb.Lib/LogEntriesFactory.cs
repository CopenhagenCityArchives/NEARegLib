using neaweb_dapper.DAL;
using neaweb_dapper.DAL.Repositories;
using neaweb_dapper.DAL.UnitsOfWork;

namespace neaweb_dapper
{
    public class LogEntriesFactory
    {
        private readonly string _connectionString;
        public LogEntriesFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public LogEntries GetNew()
        {
            // Get connection factory and connection string
            ConnectionFactory conFac = new ConnectionFactory(_connectionString);
            var connection = conFac.GetConnection;

            // Get unit of work and repositories
            var unitOfWork = new UnitOfWork(connection);
            var archiveversionMetadataRepository = new ArchiveversionMetadataRepository(unitOfWork);
            var logEntryMetadataRepository = new LogEntryRepository(unitOfWork);
            var softwareVersionRepository = new SoftwareVersionRepository(unitOfWork);

            return new LogEntries(unitOfWork, archiveversionMetadataRepository, softwareVersionRepository, logEntryMetadataRepository);
        }
    }
}
