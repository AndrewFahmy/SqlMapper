using System;
using System.Reflection;
using SqlMapper.Common.Abstracts;

namespace SqlMapper.Common.Models
{
    internal class Property
    {
        public int ResultSetIndex { get; set; }

        public string PropertyName { get; set; }

        public string ColumnName { get; set; }

        public string ParameterName { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public Type UnderlyingType { get; set; }

        public TypeInfo UnderlyingTypeInfo { get; set; }

        public IMapper Mapper { get; set; }

        public bool IsValueFromOutputParameter { get; set; }

        public Property GroupingProperty { get; set; }

        public Property PrimaryKeyProperty { get; set; }

        public MappingAttribute AttributeData { get; set; }
    }
}