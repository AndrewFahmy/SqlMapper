using System.Data.Common;
using SqlMapper.Common.Abstracts;

namespace SqlMapper.Factories
{
    internal class ParameterFactory : IFactory<DbCommand, DbParameter>
    {

        private static IFactory<DbCommand, DbParameter> _instance;
        internal static IFactory<DbCommand, DbParameter> Instance
        {
            get
            {
                _instance = _instance ?? new ParameterFactory();
                return _instance;
            }
        }


        private ParameterFactory() { }

        public DbParameter CreateInstance(DbCommand cmd)
        {
            return cmd.CreateParameter();
        }
    }
}