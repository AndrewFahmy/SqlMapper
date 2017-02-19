using System;
using System.Collections.Generic;
using System.Data;
using SqlMapper.Extensions;
using SqlMapper.Factories;
using SqlMapper.Common.Models;

namespace SqlMapper.Common
{
    internal class InternalCommand : IDisposable
    {
        private UnitofWork _unitofWork;
        private ConnectionFactory _conFactory;

        internal InternalCommand(UnitofWork unitofWork, ConnectionFactory confFactory)
        {
            _unitofWork = unitofWork;
            _conFactory = confFactory;
        }

        public void Dispose()
        {
            _unitofWork = null;
            _conFactory = null;
        }

        internal IEnumerable<CommandResult> ExecMapperCommand(string query, CommandType cmdType, params CommandParameter[] parameters)
        {
            var con = _unitofWork.UseTransaction
                ? _unitofWork.TransactionInstance.Connection
                : _conFactory.CreateInstance();

            using (var cmd = CommandFactory.Instance.CreateInstance(con))
            {
                if (_unitofWork.UseTransaction)
                    cmd.Transaction = _unitofWork.TransactionInstance;

                con.CheckConnectionState();

                cmd.CommandText = query;
                cmd.CommandType = cmdType;

                cmd.AttachParameters(parameters);

                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    var updatedParameters = cmd.GetParametersAfterExecution();

                    var resultSetIndex = -1;

                    do
                    {
                        resultSetIndex++;

                        while (reader.Read())
                        {
                            var result = new CommandResult(resultSetIndex, updatedParameters);

                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                var columnName = reader.GetName(i);

                                if (!result.Columns.ContainsKey(columnName))
                                    result.Columns.Add(columnName, reader[i]);
                            }

                            yield return result;
                        }
                    } while (reader.NextResult());
                }

                if (!_unitofWork.UseTransaction)
                    con.Dispose();
            }
        }

        internal bool ExecCommand<T>(string query, CommandType cmdType, T model)
        {
            var con = _unitofWork.UseTransaction
                ? _unitofWork.TransactionInstance.Connection
                : _conFactory.CreateInstance();

            using (var cmd = CommandFactory.Instance.CreateInstance(con))
            {
                if (_unitofWork.UseTransaction)
                    cmd.Transaction = _unitofWork.TransactionInstance;

                con.CheckConnectionState();

                cmd.CommandText = query;
                cmd.CommandType = cmdType;

                cmd.AttachParameters(model);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        internal bool ExecCommand(string query, CommandType cmdType, params CommandParameter[] parameters)
        {
            var con = _unitofWork.UseTransaction
                ? _unitofWork.TransactionInstance.Connection
                : _conFactory.CreateInstance();

            using (var cmd = CommandFactory.Instance.CreateInstance(con))
            {
                if (_unitofWork.UseTransaction)
                    cmd.Transaction = _unitofWork.TransactionInstance;

                con.CheckConnectionState();

                cmd.CommandText = query;
                cmd.CommandType = cmdType;

                cmd.AttachParameters(parameters);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        internal T ExecScalarCommand<T>(string query, CommandType cmdType, params CommandParameter[] parameters)
        {
            using (var con = _conFactory.CreateInstance())
            using (var cmd = CommandFactory.Instance.CreateInstance(con))
            {
                con.CheckConnectionState();

                cmd.CommandText = query;
                cmd.CommandType = cmdType;

                cmd.AttachParameters(parameters);

                return (T) cmd.ExecuteScalar();
            }
        }
    }
}