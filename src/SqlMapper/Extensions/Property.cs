using SqlMapper.Common;
using SqlMapper.Common.Models;
using SqlMapper.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SqlMapper.Common.Abstracts;

namespace SqlMapper.Extensions
{
    internal static class PropertyExtensions
    {
        private static readonly IList<PropertyInfo> AttribProperties = (IList<PropertyInfo>)typeof(MappingAttribute).GetRuntimeProperties();

        internal static IList<Property> GetTypeProperties(this Type model, ExcludeTypes exclusion, Property parentData = null)
        {
            var primaryKeyProp = GetPrimaryKeyProperty(model);

            var properties =
                model.GetTypeInfo()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                    .Where(p =>
                    {
                        var attrib = p.GetCustomAttribute<MappingAttribute>();
                        return attrib == null ||
                               (attrib.ExcludeFrom != exclusion && attrib.ExcludeFrom != ExcludeTypes.All);
                    })
                    .OrderBy(p => p.MetadataToken);

            var modelTypeInfo = model.GetTypeInfo();

            var propertyList = properties.Select(p => new Property
            {
                PropertyInfo = p,
                PropertyName = p.Name,
                ResultSetIndex = p.GetPropertyValueOrDefault("ResultSetIndex", -1),
                ColumnName = p.GetPropertyValueOrDefault("ColumnName", p.Name),
                UnderlyingType = p.GetActualType(),
                ParameterName = $"@{p.GetPropertyValueOrDefault("ParameterName", p.Name).Replace("@", string.Empty)}",
                IsValueFromOutputParameter = p.GetPropertyValueOrDefault("IsOutput", false),
                PrimaryKeyProperty = primaryKeyProp,
                GroupingProperty = p.GetGroupingProperty(modelTypeInfo)
            }).ToList();

            propertyList.ForEach(p =>
            {
                p.UnderlyingTypeInfo = p.UnderlyingType.GetTypeInfo();
                p.Mapper = p.UnderlyingTypeInfo.GetPropertyMapper();
                p.ResultSetIndex = p.ResultSetIndex == -1 ? parentData?.ResultSetIndex ?? 0 : p.ResultSetIndex;
            });

            return propertyList;
        }

        internal static void SetPropertyValue<T>(this Property prop, T model, object val)
        {
            prop.PropertyInfo.SetMethod.Invoke(model, new[] { val });
        }

        private static Property GetPrimaryKeyProperty(this Type model)
        {
            var property = model.GetRuntimeProperties().FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);

            if (property == null) return null;

            var prop = new Property
            {
                PropertyName = property.Name,
                ColumnName = property.GetPropertyValueOrDefault("ColumnName", property.Name),
                PropertyInfo = property,
                UnderlyingType = property.GetActualType(),
                ParameterName =
                    $"{property.GetPropertyValueOrDefault("ParameterName", property.Name).Replace("@", string.Empty)}"
            };

            prop.UnderlyingTypeInfo = prop.UnderlyingType.GetTypeInfo();
            prop.Mapper = prop.UnderlyingTypeInfo.GetPropertyMapper();

            return prop;
        }

        private static T GetPropertyValueOrDefault<T>(this PropertyInfo info, string propertyName, T defaultValue)
        {
            var attribute = info.GetCustomAttribute<MappingAttribute>();

            if (attribute == null) return defaultValue;

            var requestedProperty = AttribProperties.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

            if (requestedProperty == null) return defaultValue;

            var propValue = requestedProperty.GetValue(attribute, null);

            return propValue == null ? defaultValue : (T)propValue;
        }

        private static Type GetActualType(this PropertyInfo info)
        {
            if (info.PropertyType.IsConstructedGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(info.PropertyType);
            else
                return info.PropertyType;
        }

        private static IMapper GetPropertyMapper(this TypeInfo info)
        {
            var isValue = info.IsSealed;
            var isList = typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(info);

            if (isValue) return ValueMapper.Instance;

            return isList ? (IMapper)ListMapper.Instance : ClassMapper.Instance;
        }

        private static Property GetGroupingProperty(this PropertyInfo info, TypeInfo modelType)
        {
            var propertyName = info.GetCustomAttribute<MappingAttribute>()?.GroupBy;

            if (string.IsNullOrWhiteSpace(propertyName)) return null;

            var propData = modelType.GetProperty(propertyName);

            if (propData == null) return null;

            var property = new Property
            {
                PropertyName = propData.Name,
                ColumnName = propData.GetPropertyValueOrDefault("ColumnName", propData.Name),
                PropertyInfo = propData,
                UnderlyingType = propData.GetActualType()
            };

            property.UnderlyingTypeInfo = property.UnderlyingType.GetTypeInfo();
            property.Mapper = property.UnderlyingTypeInfo.GetPropertyMapper();

            return property;
        }
    }
}