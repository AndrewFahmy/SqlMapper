using SqlMapper.Common.Abstracts;
using SqlMapper.Tests.Mocks;
using SqlMapper.Tests.Models;
using SqlMapper.Tests.ViewModel;

namespace SqlMapper.Tests
{
    public class TestContext : ContextBase<ConnectionMock>
    {
        public TestContext() : base("connection string test")
        {
        }

        public IRepository<SingleResultSetModel> SingleResultSetMapping { get; set; }

        public IRepository<MultiResultViewModel> MultiResultSetMapping { get; set; }

        public IRepository<MultiResultViewModel2> MultiResultSetMapping2 { get; set; }

        public IRepository<SingleResultSetWithGroupModel> SingleResultSetWithGroupModel { get; set; }
    }
}