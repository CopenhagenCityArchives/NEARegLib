using System;
using System.Data;

namespace neaweb_dapper.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        readonly IDbConnection _connection;
        IDbTransaction _dbTransaction;

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        public UnitOfWork(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public void StartTransaction()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            _dbTransaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }
        public void RollBack()
        {
            _dbTransaction.Rollback();
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
