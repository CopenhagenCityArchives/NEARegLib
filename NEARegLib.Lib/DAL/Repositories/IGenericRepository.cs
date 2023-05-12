using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> RetrieveAll();
        Task<TEntity> Retrieve(int Id);
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entity);
    }

}
