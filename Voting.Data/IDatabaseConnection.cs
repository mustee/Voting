using System.Data.Common;

namespace Voting.Data
{
    public interface IDatabaseConnection
    {
        DbConnection Connection { get; }
    }
}
