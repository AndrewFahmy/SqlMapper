using System.Data;
using System.Data.Common;

namespace SqlMapper.Tests.Mocks
{
    public class TransactionMock : DbTransaction
    {
        private ConnectionMock _mockCon;
        private readonly IsolationLevel _level;

        public TransactionMock(ConnectionMock mockCon, IsolationLevel level)
        {
            _mockCon = mockCon;
            _level = level;
        }

        public override void Commit()
        {
            //not needed
        }

        public override void Rollback()
        {
            //not needed
        }

        protected override DbConnection DbConnection => _mockCon;
        public override IsolationLevel IsolationLevel => _level;
    }
}