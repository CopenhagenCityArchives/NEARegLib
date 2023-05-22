using NEARegLib.Models;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        public Task<Location> GetLocationByPath(string path);
    }
}