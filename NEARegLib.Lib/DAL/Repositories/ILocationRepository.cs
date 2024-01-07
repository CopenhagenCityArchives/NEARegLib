using NEARegLib.Models;

namespace NEARegLib.DAL.Repositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        public Location GetLocationByPath(string path);
    }
}