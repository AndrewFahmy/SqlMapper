using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using SqlMapper.Common;
using SqlMapper.Factories;
using System.Linq;
using System.Runtime.CompilerServices;
using SqlMapper.Common.Abstracts;
using SqlMapper.Common.Models;
using SqlMapper.Extensions;

namespace SqlMapper
{
    public class ContextBase<T> where T : DbConnection
    {
        private readonly ConnectionFactory _conFactory;

        protected ContextBase(string connectionString)
        {
            _conFactory = new ConnectionFactory(connectionString, typeof(T));
            InitializeObjectProperties();
        }

        private void InitializeObjectProperties()
        {
            var properties = GetType().GetTypeInfo().GetProperties()
                .Where(p => p.PropertyType.Name == typeof(IRepository<>).Name).ToList();


            foreach (var prop in properties)
            {
                var underlyingType = prop.PropertyType.GetTypeInfo().GetGenericArguments()[0];

               underlyingType.CacheType();

                prop.GetSetMethod()?.Invoke(this, new[] { Activator.CreateInstance(typeof(Repository<>).MakeGenericType(underlyingType), _conFactory) });
            }
        }

        /// <summary>
        /// Gets data from database and returnes it to the user.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <param name="type">Type of command</param>
        /// <param name="parameters">Parameters used with the the query.</param>
        /// <returns>A list of the returned values and updated parameters.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<CommandResult> GetData(string query, CommandType type, params CommandParameter[] parameters)
        {
            using (var internalCmd = new InternalCommand(_conFactory))
            {
                foreach (var result in internalCmd.ExecMapperCommand(query, type, parameters))
                    yield return result;
            }
        }

        /// <summary>
        /// Get value of first column in first row from the result set.
        /// </summary>
        /// <typeparam name="TResult">Supplied type to be usedin conversion.</typeparam>
        /// <param name="query">Query to execute.</param>
        /// <param name="type">Type of command</param>
        /// <param name="parameters">Parameters to be used with the query.</param>
        /// <returns>The aquired value after conversion.</returns>
        public TResult GetScalar<TResult>(string query, CommandType type, params CommandParameter[] parameters)
        {
            using (var internalCmd = new InternalCommand(_conFactory))
            {
                return internalCmd.ExecScalarCommand<TResult>(query, type, parameters);
            }
        }
    }
}