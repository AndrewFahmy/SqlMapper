using System.Collections.Generic;
using System.Data;
using SqlMapper.Common.Models;

namespace SqlMapper.Common.Abstracts
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Create or Update a record.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <param name="type">Type of command</param>
        /// <param name="model">Model containing data.</param>
        /// <returns>Boolean value whether the record was created/updated or not.</returns>
        bool Save(string query, CommandType type, T model);

        /// <summary>
        /// Delete existing record.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <param name="type">Type of command</param>
        /// <param name="parameters">Parameters to be passed to database.</param>
        /// <returns>Boolean value whether the record was deleted or not.</returns>
        bool Delete(string query, CommandType type, params CommandParameter[] parameters);

        /// <summary>
        /// Executes the query and returnes the supplied data.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <param name="type">Type of command</param>
        /// <param name="parameters">Parameters to be passed to database.</param>
        /// <returns>A collection of the model after mapping.</returns>
        IList<T> Search(string query, CommandType type, params CommandParameter[] parameters);

        /// <summary>
        /// Executes the query and returnes first row of the supplied data.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <param name="type">Type of command</param>
        /// <param name="parameters">Parameters to be passed to database.</param>
        /// <returns>A model instance after mapping.</returns>
        T GetSingle(string query, CommandType type, params CommandParameter[] parameters);
    }
}