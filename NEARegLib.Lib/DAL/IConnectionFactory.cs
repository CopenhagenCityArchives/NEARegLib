using System.Data;

namespace NEARegLib.DAL
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
