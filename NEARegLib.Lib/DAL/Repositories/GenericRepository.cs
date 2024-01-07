using Dapper;
using NEARegLib.DAL.UnitsOfWork;
using System.Collections.Generic;
using System.Linq;
using static Dapper.SqlMapper;

namespace NEARegLib.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork unitOfWork;

        abstract protected string GetAllStatement { get; }
        abstract protected string GetStatement { get; }
        abstract protected string InsertStatement { get; }
        abstract protected string UpdateStatement { get; }

        protected string GetLastInsertIdStatement
        {
            get
            {
                return "SELECT LAST_INSERT_ID();";
            }
        }

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual IEnumerable<TEntity> RetrieveAll()
        {
            return unitOfWork.Connection.Query<TEntity>(GetAllStatement, transaction: unitOfWork.Transaction);
        }

        public virtual TEntity Retrieve(int id)
        {
            var list = unitOfWork.Connection.Query<TEntity>(GetStatement, new { Id = id }, transaction: unitOfWork.Transaction);
            return list.FirstOrDefault();
        }

        public virtual TEntity Create(TEntity entity)
        {
            var result = unitOfWork.Connection.Query<int>(InsertStatement + GetLastInsertIdStatement, entity, transaction: unitOfWork.Transaction);

            return Retrieve(result.First());
        }

        public void Update(TEntity entity)
        {
            unitOfWork.Connection.Query<TEntity>(UpdateStatement, entity, transaction: unitOfWork.Transaction);
        }
    }

}
