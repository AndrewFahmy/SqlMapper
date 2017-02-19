using System;
using System.Linq;
using SqlMapper.Common.Abstracts;
using SqlMapper.Common.Models;

namespace SqlMapper.Helpers
{
    internal class ValueMapper : IMapper
    {
        private static ValueMapper _instance;
        internal static ValueMapper Instance
        {
            get
            {
                _instance = _instance ?? new ValueMapper();
                return _instance;
            }
        }


        private ValueMapper() { }


        public void Map<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            if (data.ResultSetIndex != propData.ResultSetIndex) return;

            if (propData.IsValueFromOutputParameter)
            {
                var parameter = GetParameterByName(data.Parameters, propData.ParameterName);

                if (parameter != null)
                    SetPropertyValue(model, propData, parameter.Value);

            }
            else
                SetPropertyValue(model, propData, data.Columns[propData.ColumnName]);
        }


        private static CommandParameter GetParameterByName(CommandParameter[] parameters, string parameterName)
        {
            if (parameters == null || !parameters.Any()) return null;

            return parameters.FirstOrDefault(p => string.Equals(parameterName, p.Name, StringComparison.OrdinalIgnoreCase));
        }

        private static void SetPropertyValue<T, TVal>(T model, Property prop, TVal val)
        {
            if (val is DBNull) return;

            var convInstance = val as IConvertible;

            if (convInstance != null)
                prop.PropertyInfo.SetMethod.Invoke(model, new[] { convInstance.ToType(prop.UnderlyingType, null) });
            else
                prop.PropertyInfo.SetMethod.Invoke(model, new[] { Convert.ChangeType(val, prop.UnderlyingType) });
        }
    }
}