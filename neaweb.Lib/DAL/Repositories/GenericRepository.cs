﻿using Dapper;
using neaweb_dapper.DAL.UnitsOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace neaweb_dapper.DAL.Repositories
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

        public virtual async Task<IEnumerable<TEntity>> RetrieveAll()
        {
            return await unitOfWork.Connection.QueryAsync<TEntity>(GetAllStatement);
        }

        public virtual async Task<TEntity> Retrieve(int id)
        {
            var list = await unitOfWork.Connection.QueryAsync<TEntity>(GetStatement, new { Id = id }, unitOfWork.Transaction);
            return list.FirstOrDefault();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            var result = await unitOfWork.Connection.QueryAsync<int>(InsertStatement + GetLastInsertIdStatement, entity, unitOfWork.Transaction);

            return await Retrieve(result.First());
        }

        public async Task Update(TEntity entity)
        {
            await unitOfWork.Connection.QueryAsync<TEntity>(UpdateStatement, entity, unitOfWork.Transaction);
            return;
        }
    }

}
