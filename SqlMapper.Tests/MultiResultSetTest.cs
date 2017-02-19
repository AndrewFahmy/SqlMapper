using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper.Tests.Extensions;
using SqlMapper.Tests.Mocks.DataMock;
using System.Linq;

namespace SqlMapper.Tests
{
    [TestClass]
    public class MultiResultSetTest
    {
        private TestContext _ctx;

        public MultiResultSetTest()
        {
            _ctx = new TestContext();
        }


        [TestMethod]
        public void CommandResultShouldReturnTheSameListValuesBeforeMapping()
        {
            //assign
            
            var dataToExpect = TestDataSupplier.GetMultiResultSetQueryData();
            var mappedData = _ctx.GetData("test query 1 multi", CommandType.Text);

            //arrange
            var actualData = dataToExpect.SelectMany(s => s.ToKeyValuePairs()).ToList();
            var mappedColumns = mappedData.SelectMany(s => s.Columns.ToKeyValuePairs()).ToList();

            //assert
            CollectionAssert.AreEquivalent(mappedColumns, actualData);
        }

        [TestMethod]
        public void MappedObjectShouldContainTheSameMultiSetValues()
        {
            //assign
            var validData = TestDataSupplier.GetMultiResultSetMappedData();

            //arrange
            var mappedData = _ctx.MultiResultSetMapping.GetSingle("test query 1 multi", CommandType.Text);

            //assert
            Assert.AreEqual(mappedData, validData);
        }

        [TestMethod]
        public void CommandResultShouldReturnTheSameListValuesWithMultiRowsBeforeMapping()
        {
            //assign
            var dataToExpect = TestDataSupplier.GetMultiResultSetQueryDataWithMultipleRows();
            var mappedData = _ctx.GetData("test query 2 multi", CommandType.Text);

            //arrange
            var actualData = dataToExpect.SelectMany(s => s.ToKeyValuePairs()).ToList();
            var mappedColumns = mappedData.SelectMany(s => s.Columns.ToKeyValuePairs()).ToList();

            //assert
            CollectionAssert.AreEquivalent(mappedColumns, actualData);
        }

        [TestMethod]
        public void MappedObjectShouldContainTheSameMultiSetValuesWithMultiRows()
        {
            //assign
            var validData = TestDataSupplier.GetMultiResultSetMappedDataWithMultiRows();

            //arrange
            var mappedData = _ctx.MultiResultSetMapping2.GetSingle("test query 2 multi", CommandType.Text);

            //assert
            Assert.AreEqual(mappedData, validData);
        }
    }
}