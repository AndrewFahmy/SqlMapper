using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using SqlMapper.Tests.Mocks.DataMock;
using SqlMapper.Tests.Mocks.Models;

namespace SqlMapper.Tests.Mocks
{
    public class ReaderMock : DbDataReader
    {
        private int _currentResultSetIndex = 0;
        private int _currentindex = -1;
        private List<ResultSetModel> _dataToReturn;
        private ReaderDataSupplier _supplier;

        public ReaderMock(string query)
        {
            _supplier = new ReaderDataSupplier();
            _dataToReturn = new List<ResultSetModel>();
            GetMockData(query);
        }

        private void GetMockData(string query)
        {
            switch (query.ToLowerInvariant())
            {
                case "test query 1":
                    _dataToReturn.Add(_supplier.GetSingleResultSetQueryData());
                    break;

                case "test query 1 multi":
                    _dataToReturn.AddRange(_supplier.GetMultiResultSetQueryData());
                    break;

                case "test query 2":
                    _dataToReturn.Add(_supplier.GetSingleResultSetQueryWithMultiRows());
                    break;

                case "test query 2 multi":
                    _dataToReturn.AddRange(_supplier.GetMultiResultSetWithMultiRows());
                    break;

                case "test query 3":
                    _dataToReturn.Add(_supplier.GetSingleResultSetGroupped());
                    break;
            }
        }


        public override bool GetBoolean(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override byte GetByte(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new System.NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new System.NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override decimal GetDecimal(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator GetEnumerator()
        {
            return _dataToReturn.GetEnumerator();
        }

        public override bool NextResult()
        {
            ++_currentResultSetIndex;

            if (_dataToReturn.Count <= _currentResultSetIndex) return false;

            _currentindex = -1;

            return true;
        }

        public override bool Read()
        {
            ++_currentindex;

            if(_dataToReturn[_currentResultSetIndex].Rows.Count <= _currentindex) return false;

            return true;
        }

        public override int Depth => 0;

        public override bool IsClosed
            =>
            _dataToReturn.Count <= _currentResultSetIndex ||
            _dataToReturn[_currentResultSetIndex].Rows.Count <= _currentindex;

        public override int RecordsAffected => _dataToReturn[_currentResultSetIndex].Rows.Count;

        public override object this[string name]
        {
            get
            {
                var columnIndex = _dataToReturn[_currentResultSetIndex].Columns.IndexOf(name);
                return _dataToReturn[_currentResultSetIndex].Rows[_currentindex].ColumnValues[columnIndex];
            }
        }

        public override object this[int ordinal]
            => _dataToReturn[_currentResultSetIndex].Rows[_currentindex].ColumnValues[ordinal];

        public override int FieldCount => _dataToReturn[_currentResultSetIndex].Columns.Count;
        public override bool HasRows => _dataToReturn[_currentResultSetIndex].Rows.Count > 0;

        public override bool IsDBNull(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override int GetValues(object[] values)
        {
            throw new System.NotImplementedException();
        }

        public override object GetValue(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override string GetString(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override int GetOrdinal(string name)
        {
            throw new System.NotImplementedException();
        }

        public override string GetName(int ordinal) => _dataToReturn[_currentResultSetIndex].Columns[ordinal];

        public override long GetInt64(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override int GetInt32(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override short GetInt16(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override float GetFloat(int ordinal)
        {
            throw new System.NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new System.NotImplementedException();
        }
    }
}