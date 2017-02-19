using System.Collections.Generic;

namespace SqlMapper.Common.Models
{
    /// <summary>
    /// Model containing the result data for a single row.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// A key-value pair of all columns and their values.
        /// </summary>
        public Dictionary<string, object> Columns { get; private set; }


        /// <summary>
        /// All used parameters in the query.
        /// Including updated output parameters if any.
        /// </summary>
        public CommandParameter[] Parameters { get; private set; }

        /// <summary>
        /// The result set index of the current row.
        /// This is a zero based index.
        /// </summary>
        public int ResultSetIndex { get;  private set; }


        public CommandResult(int resultSetIndex, CommandParameter[] parameters)
        {
            Columns = new Dictionary<string, object>();
            ResultSetIndex = resultSetIndex;
            Parameters = parameters;
        }
    }
}