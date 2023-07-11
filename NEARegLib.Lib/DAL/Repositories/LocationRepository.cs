using NEARegLib.Models;
using NEARegLib.DAL.UnitsOfWork;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NEARegLib.DAL.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        protected override string GetAllStatement
        {
            get
            {
                return "SELECT id as Id, server as Server, netvaerks_navn as Path FROM arkiveringsversion_filarkiv WHERE 1";
            }
        }

        protected override string GetStatement
        {
            get
            {
                return "SELECT id as Id, server as Server, netvaerks_navn as Path FROM arkiveringsversion_filarkiv WHERE ID = @Id";
            }
        }

        protected override string InsertStatement
        {
            get
            {
                throw new NotImplementedException("Unsupported: Cannot insert locations");
            }
        }

        protected override string UpdateStatement
        {
            get
            {
                throw new NotImplementedException("Unsupported: Cannot update locations");
            }
        }

        public LocationRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        /// <summary>
        /// Get a location based on its path. An exception is thrown if more than 1 location is found
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A Task that returns a Location object if it exists otherwise null</returns>
        /// <exception cref="ArgumentException"></exception>
        public Location GetLocationByPath(string path)
        {
            var locations = RetrieveAll();

            var curLocation = locations.Where(l => path.ToLower().Contains(l.Path.ToLower())).FirstOrDefault();

            if (curLocation == null)
            {
                throw new ArgumentException($"No location found that matches {path}. The archiveversion cannot be updated");
            }

            return curLocation;
        }
    }
}
