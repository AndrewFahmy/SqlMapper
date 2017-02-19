using System.Data;

namespace SqlMapper.Common.Models
{
    /// <summary>
    /// Define a parameter to be used by the mapper.
    /// </summary>
    public class CommandParameter
    {
        /// <summary>
        /// Specify parameter name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specify parameter value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Specify parameter direction.
        /// </summary>
        public ParameterDirection Direction { get; set; }



        public CommandParameter(string name, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            Name = name;

            Value = value;

            Direction = direction;
        }
    }
}