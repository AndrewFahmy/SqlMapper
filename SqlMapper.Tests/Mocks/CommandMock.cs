using System.Data;
using System.Data.Common;

namespace SqlMapper.Tests.Mocks
{
    public class CommandMock : DbCommand
    {
        private ParameterCollectionMock _parameters;

        public CommandMock()
        {
            _parameters = new ParameterCollectionMock();
        }

        public override void Cancel()
        {
            //not needed
        }

        public override int ExecuteNonQuery()
        {
            return 1;
        }

        public override object ExecuteScalar()
        {
            return new {};
        }

        public override void Prepare()
        {
            //not needed
        }

        public override string CommandText { get; set; }
        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }
        protected override DbConnection DbConnection { get; set; }
        protected override DbParameterCollection DbParameterCollection => _parameters;
        protected override DbTransaction DbTransaction { get; set; }
        public override bool DesignTimeVisible { get; set; }


        protected override DbParameter CreateDbParameter()
        {
            return new ParameterMock();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return new ReaderMock(CommandText);
        }
    }
}