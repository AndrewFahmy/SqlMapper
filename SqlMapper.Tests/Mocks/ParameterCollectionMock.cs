using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using NuGet.Packaging;

namespace SqlMapper.Tests.Mocks
{
    public class ParameterCollectionMock : DbParameterCollection
    {
        private IList<DbParameter> _parameters;

        public ParameterCollectionMock()
        {
            _parameters = new List<DbParameter>();
        }



        public override int Add(object value)
        {
            _parameters.Add((DbParameter)value);
            return _parameters.Count;
        }

        public override void AddRange(Array values)
        {
            foreach (var value in values)
                Add(value);
        }

        public override void Clear()
        {
            _parameters.Clear();
        }

        public override void RemoveAt(int index)
        {
            _parameters.RemoveAt(index);
        }

        public override void Remove(object value)
        {
            _parameters.Remove((DbParameter)value);
        }

        public override void Insert(int index, object value)
        {
            _parameters.Insert(index, (DbParameter)value);
        }

        public override int IndexOf(object value)
        {
            return _parameters.IndexOf((DbParameter)value);
        }

        public override IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public override int Count => _parameters.Count;
        public override object SyncRoot => "Not Implemented";

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            var parameter =
                _parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase));

            if (parameter != null)
                Remove(parameter);

            Add(value);
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            if (index >= _parameters.Count) return;


            var parameter = _parameters[index];

            if (parameter != null)
                Remove(parameter);

            Add(value);
        }

        public override void RemoveAt(string parameterName)
        {
            var parameter =
                _parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase));

            if(parameter != null)
                Remove(parameter);
        }

        public override int IndexOf(string parameterName)
        {
            var parameter =
                _parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase));

            if (parameter == null)
                return -1;

            return _parameters.IndexOf(parameter);
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return _parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase));
        }

        protected override DbParameter GetParameter(int index)
        {
            if (index >= _parameters.Count) return null;

            return _parameters[index];
        }

        public override void CopyTo(Array array, int index)
        {
            //not needed
        }

        public override bool Contains(string value)
        {
            //not needed
            return false;
        }

        public override bool Contains(object value)
        {
            //not needed
            return false;
        }
    }
}