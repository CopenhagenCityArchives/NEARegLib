using System.Data;

namespace neaweb_dapper.DAL
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
