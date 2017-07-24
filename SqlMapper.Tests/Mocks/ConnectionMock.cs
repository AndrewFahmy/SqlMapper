using System.Data;
using System.Data.Common;

namespace SqlMapper.Tests.Mocks
{
    public class ConnectionMock : DbConnection
    {
        private string _connectionString;

        public ConnectionMock(string connectionString)
        {
            _connectionString = connectionString;
        }

        private ConnectionState _state;

        // ReSharper disable once OptionalParameterHierarchyMismatch
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return new TransactionMock(this, isolationLevel);
        }

        public override void Close()
        {
            ConnectionString = null;
            _state = ConnectionState.Closed;
        }

        public override void Open()
        {
            _state = ConnectionState.Open;
        }

        public override string ConnectionString {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public override string Database => "Mock";
        public override ConnectionState State => _state;
        public override string DataSource => "Mock";
        public override string ServerVersion => "1.0";

        protected override DbCommand CreateDbCommand()
        {
            return new CommandMock();
        }

        public override void ChangeDatabase(string databaseName)
        {
            //not needed
        }
    }
}