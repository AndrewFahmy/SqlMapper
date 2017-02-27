using SqlMapper.Common;
using SqlMapper.Common.Abstracts;
using SqlMapper.Common.Models;
using SqlMapper.Extensions;
using SqlMapper.Factories;
using SqlMapper.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace SqlMapper
{
    internal class Repository<T> : IRepository<T>
    {
        private readonly ConnectionFactory _conFactory;

        public Repository(ConnectionFactory conFactory)
        {
            _conFactory = conFactory;
        }

        public bool Save(string query, CommandType type, T model)
        {
            using (var internalCmd = new InternalCommand(_conFactory))
            {
                return internalCmd.ExecCommand(query, type, model);
            }
        }

        public bool Save(string query, CommandType type, params CommandParameter[] parameters)
        {
            using (var internalCmd = new InternalCommand(_conFactory))
            {
                return internalCmd.ExecCommand(query, type, parameters);
            }
        }

        public bool Delete(string query, CommandType type, params CommandParameter[] parameters)
        {
            using (var internalCmd = new InternalCommand(_conFactory))
            {
                return internalCmd.ExecCommand(query, type, parameters);
            }
        }

        public IList<T> Search(string query, CommandType type, params CommandParameter[] parameters)
        {
            var mapperMain = new MapperMain<T>();

            var modelProperties = typeof(T).GetTypeProperties(ExcludeTypes.Select);

            using (var internalCmd = new InternalCommand(_conFactory))
            {
                foreach (var result in internalCmd.ExecMapperCommand(query, type, parameters))
                {
                    var instance = Activator.CreateInstance<T>();
                    mapperMain.Map(result, instance, modelProperties);
                }
            }

            return mapperMain.ItemsList;
        }

        public T GetSingle(string query, CommandType type, params CommandParameter[] parameters)
        {
            var mapperMain = new MapperMain<T>();

            var modelProperties = typeof(T).GetTypeProperties(ExcludeTypes.Select);

            var instance = Activator.CreateInstance<T>();

            using (var internalCmd = new InternalCommand(_conFactory))
            {
                foreach (var result in internalCmd.ExecMapperCommand(query, type, parameters))
                    mapperMain.Map(result, instance, modelProperties);
            }

            return instance;
        }
    }
}