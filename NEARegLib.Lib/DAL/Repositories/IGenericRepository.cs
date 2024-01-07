using System.Collections.Generic;

namespace NEARegLib.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> RetrieveAll();
        TEntity Retrieve(int Id);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
    }

}
