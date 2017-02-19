using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using SqlMapper.Common;
using SqlMapper.Factories;
using SqlMapper.Common.Models;

namespace SqlMapper.Extensions
{
    internal static class CommandExtensions
    {
        internal static void AttachParameters(this DbCommand cmd, CommandParameter[] parameters)
        {
            cmd.Parameters.Clear();

            foreach (var parameter in parameters)
            {
                var commandParameter = ParameterFactory.Instance.CreateInstance(cmd);
                commandParameter.ParameterName = parameter.Name;
                commandParameter.Direction = parameter.Direction;
                commandParameter.Value = parameter.Value ?? DBNull.Value;

                cmd.Parameters.Add(commandParameter);
            }
        }

        internal static CommandParameter[] GetParametersAfterExecution(this DbCommand cmd)
        {
            var returnList = new List<CommandParameter>(cmd.Parameters.Count);

            returnList.AddRange(cmd.Parameters.Cast<DbParameter>().Select(p => new CommandParameter(p.ParameterName, p.Value, p.Direction)));

            return returnList.ToArray();
        }

        internal static void AttachParameters<T>(this DbCommand cmd, T model)
        {
            cmd.Parameters.Clear();

            var modelProperties = typeof(T).GetTypeProperties(ExcludeTypes.Crud);

            foreach (var prop in modelProperties)
            {
                var parameter = ParameterFactory.Instance.CreateInstance(cmd);
                var propValue = prop.PropertyInfo.GetValue(model, null);

                parameter.ParameterName = prop.ParameterName;

                if (propValue != null)
                    SetParameterValue(parameter, prop, propValue);
                else
                    parameter.Value = DBNull.Value;

                cmd.Parameters.Add(parameter);
            }
        }

        private static void SetParameterValue<T>(DbParameter parameter, Property propData, T value)
        {
            var convertableInstance = value as IConvertible;

            if (convertableInstance != null)
                parameter.Value = convertableInstance.ToType(propData.UnderlyingType, null);
            else if (value != null)
                parameter.Value = Convert.ChangeType(value, propData.UnderlyingType);
            else
                parameter.Value = DBNull.Value;
        }
    }
}