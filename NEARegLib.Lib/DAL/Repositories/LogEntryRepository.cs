using NEARegLib.Models;
using NEARegLib.DAL.UnitsOfWork;
using System;

namespace NEARegLib.DAL.Repositories
{
    public class LogEntryRepository : GenericRepository<LogEntry>
    {
        protected override string GetAllStatement
        {
            get
            {
                return "SELECT * FROM LOG WHERE 1;";
            }
        }

        protected override string GetStatement
        {
            get
            {
                return "SELECT * FROM LOG WHERE ID = @Id;";
            }
        }

        protected override string InsertStatement
        {
            get
            {
                return "INSERT INTO LOG (ID, gruppe, AVID, beskrivelse, ts, errors_occurred, software_version, user_ID) " +
                "VALUES " +
                "(@Id, @Type, @ArchiveversionId, @Description, NOW(), @ErrorsOccurred, @SoftwareVersionId, @UserId);";
            }
        }

        protected override string UpdateStatement
        {
            get
            {
                throw new NotImplementedException("Unsupported: Cannot update log entries");
            }
        }

        public LogEntryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /*    public override async Task<LogEntry> Create(LogEntry entity)
            {
                var result = await unitOfWork.Connection.QueryAsync<LogEntry>(
                    InsertStatement + GetLastInsertIdStatement, 
                    new { entity.Id, entity.Type, ArchiveversionId = entity.ArchiveversionMetadata.Id, description = entity.Description, status = entity.Status, SoftwareVersion = entity.SoftwareVersion.Id, entity.UserId }, 
                    unitOfWork.Transaction);

                return result.First();
            }*/
    }
}
