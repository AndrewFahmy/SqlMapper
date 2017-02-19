using System.Data;
using System.Data.Common;

namespace SqlMapper.Extensions
{
    internal static class ConnectionExtensions
    {
        internal static void CheckConnectionState(this DbConnection con)
        {
            if (con.State != ConnectionState.Open) con.Open();
        }
    }
}