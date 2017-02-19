using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper.Tests.Extensions;
using SqlMapper.Tests.Mocks.DataMock;

namespace SqlMapper.Tests
{
    [TestClass]
    public class SingleResultSetTest
    {
        private TestContext _ctx;

        public SingleResultSetTest()
        {
            _ctx = new TestContext();
        }


        [TestMethod]
        public void CommandResultShouldReturnTheSameValueBeforeMapping()
        {
            //assign
            var dataToExpect = TestDataSupplier.GetSingleResultSetQueryData();

            //arrange
            var mappedData = _ctx.GetData("test query 1", CommandType.Text).FirstOrDefault();

            //assert
            CollectionAssert.AreEquivalent(mappedData.Columns.ToList(), dataToExpect.ToList());
        }

        [TestMethod]
        public void MappedObjectShouldContainTheSameValues()
        {
            //assign

            var validData = TestDataSupplier.GetSingleResultSetMappedData();

            //arrange
            var mappedData = _ctx.SingleResultSetMapping.GetSingle("test query 1", CommandType.Text);

            //assert
            Assert.AreEqual(mappedData, validData);
        }

        [TestMethod]
        public void CommandResultShouldReturnTheSameValueBeforeMappingWithMultiRows()
        {
            //assign
            var dataToExpect = TestDataSupplier.GetSingleResultSetQueryWithMultiRows();
            var mappedData = _ctx.GetData("test query 2", CommandType.Text);

            //arrange
            var actualData = dataToExpect.SelectMany(s => s.ToKeyValuePairs()).ToList();
            var data = mappedData.SelectMany(s => s.Columns.ToKeyValuePairs()).ToList();

            //assert
            CollectionAssert.AreEquivalent(data, actualData);
        }


        [TestMethod]
        public void MappedObjectShouldContainTheSameMultiRowsValues()
        {
            //assign
            var validData = TestDataSupplier.GetSingleResultSetMappedWithMultiRows();

            //arrange
            var mappedData = _ctx.SingleResultSetMapping.Search("test query 2", CommandType.Text).ToList();

            //assert
            CollectionAssert.AreEqual(mappedData, validData);
        }


        [TestMethod]
        public void CommandResultShouldReturnTheSameValueBeforeMappingWithGroupData()
        {
            //assign
            var dataToExpect = TestDataSupplier.GetSingleResultSetWithGrouppedData();
            var mappedData = _ctx.GetData("test query 3", CommandType.Text);

            //arrange
            var actualData = dataToExpect.SelectMany(s => s.ToKeyValuePairs()).ToList();
            var data = mappedData.SelectMany(s => s.Columns.ToKeyValuePairs()).ToList();

            //assert
            CollectionAssert.AreEquivalent(data, actualData);
        }


        [TestMethod]
        public void MappedObjectShouldContainTheSameGrouppedValues()
        {
            //assign
            var validData = TestDataSupplier.GetSingleResultSetMappedDataWithGrouppedData();

            //arrange
            var mappedData = _ctx.SingleResultSetWithGroupModel.GetSingle("test query 3", CommandType.Text);

            //assert
            Assert.AreEqual(mappedData, validData);
        }
    }
}