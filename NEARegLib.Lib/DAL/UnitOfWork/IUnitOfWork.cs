using System.Data;

namespace NEARegLib.DAL.UnitsOfWork
{
    public interface IUnitOfWork
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; }
        void StartTransaction();
        void RollBack();
        void Commit();
    }
}
