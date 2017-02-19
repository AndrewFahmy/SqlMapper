using System.Data.Common;
using SqlMapper.Common.Abstracts;

namespace SqlMapper.Factories
{
    internal class CommandFactory : IFactory<DbConnection, DbCommand>
    {
        private static IFactory<DbConnection, DbCommand> _instance;
        internal static IFactory<DbConnection, DbCommand> Instance
        {
            get
            {
                _instance = _instance ?? new CommandFactory();
                return _instance;
            }
        }



        private CommandFactory() { }

        public DbCommand CreateInstance(DbConnection con)
        {
            return con.CreateCommand();
        }
    }
}