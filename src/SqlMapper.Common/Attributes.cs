using System;

namespace SqlMapper.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class MappingAttribute : Attribute
    {
        /// <summary>
        /// Use this name when mapping from a select query.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Exclude from mapping on specified type.
        /// </summary>
        public ExcludeTypes ExcludeFrom { get; set; } = ExcludeTypes.None;

        /// <summary>
        /// Use this name when creating a parameter of model property.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Group data by the specified property name.
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        /// Get the property value from output parameters.
        /// </summary>
        public bool IsOutput { get; set; } = false;

        /// <summary>
        /// Maps this property from data in another result set.
        /// This is a zero based index.
        /// </summary>
        public int ResultSetIndex { get; set; } = -1;
    }
}