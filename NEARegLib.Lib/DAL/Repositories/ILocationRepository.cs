using NEARegLib.Models;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        public Location GetLocationByPath(string path);
    }
}