using System;
using System.Data.Common;
using SqlMapper.Common.Abstracts;

namespace SqlMapper.Factories
{
    internal class ConnectionFactory : IFactory<DbConnection>
    {
        private readonly string _connectionString;
        private readonly Type _connectionType;

        internal ConnectionFactory(string connectionString, Type connectionType)
        {
            _connectionString = connectionString;
            _connectionType = connectionType;
        }

        public DbConnection CreateInstance()
        {
            return (DbConnection)Activator.CreateInstance(_connectionType, _connectionString);
        }
    }
}