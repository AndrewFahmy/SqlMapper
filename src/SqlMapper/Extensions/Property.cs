using Microsoft.Extensions.Caching.Memory;
using SqlMapper.Common;
using SqlMapper.Common.Abstracts;
using SqlMapper.Common.Models;
using SqlMapper.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlMapper.Extensions
{
    internal static class PropertyExtensions
    {
        internal static void CacheType(this Type model)
        {
            var entryValue = ContextCommon.Cache.GetOrCreate(model.FullName, item => item.Value = null);

            if (entryValue == null)
                ContextCommon.Cache.Set(model.FullName, model.GetTypeDataForCache());
        }

        internal static IList<Property> GetTypeProperties(this Type model, ExcludeTypes exclude, Property parentData = null)
        {
            var allProperties = ContextCommon.Cache.Get<IList<Property>>(model.FullName);

            if (allProperties == null)
            {
                model.CacheType();

                allProperties = ContextCommon.Cache.Get<IList<Property>>(model.FullName);
            }

            var filteredProperties = allProperties.Where(p => p.AttributeData == null || p.AttributeData.ExcludeFrom != exclude).ToList();

            filteredProperties.ForEach(p =>
            {
                p.ResultSetIndex = parentData?.ResultSetIndex ?? p.OriginalResultSetIndex;
            });

            return filteredProperties;
        }

        private static IList<Property> GetTypeDataForCache(this Type model)
        {
            var primaryKeyProp = model.GetPrimaryKeyProperty();

            var properties =
                model.GetTypeInfo()
                    .GetProperties()
                    .Where(p =>
                    {
                        var attrib = p.GetCustomAttribute<MappingAttribute>();
                        return attrib == null || attrib.ExcludeFrom != ExcludeTypes.All;
                    })
                    .OrderBy(p => p.MetadataToken);

            var modelTypeInfo = model.GetTypeInfo();

            var propertyList = properties.Select(p =>
            {
                var attrib = p.GetCustomAttribute<MappingAttribute>();
                var actualType = p.GetActualType();
                var typeInfo = actualType.GetTypeInfo();

                return new Property
                {
                    AttributeData = attrib,
                    PropertyInfo = p,
                    PropertyName = p.Name,
                    OriginalResultSetIndex = attrib?.ResultSetIndex ?? 0,
                    ColumnName = attrib?.ColumnName ?? p.Name,
                    UnderlyingType = attrib?.OverridePropertyType ?? actualType,
                    UnderlyingTypeInfo = typeInfo,
                    ParameterName =
                        $"@{(attrib?.ParameterName ?? p.Name).Replace("@", string.Empty)}",
                    IsValueFromOutputParameter = attrib?.IsOutput ?? false,
                    PrimaryKeyProperty = primaryKeyProp,
                    GroupingProperty = GetGroupingProperty(modelTypeInfo, attrib),
                    Mapper = typeInfo.GetPropertyMapper()
                };
            }).ToList();

            return propertyList;
        }

        private static Property GetPrimaryKeyProperty(this Type model)
        {
            var property = model.GetRuntimeProperties().FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);

            if (property == null) return null;

            var attrib = property.GetCustomAttribute<MappingAttribute>();

            var prop = new Property
            {
                PropertyName = property.Name,
                ColumnName = attrib?.ColumnName ?? property.Name,
                PropertyInfo = property,
                UnderlyingType = property.GetActualType(),
                ParameterName =
                    $"@{(attrib?.ParameterName ?? property.Name).Replace("@", string.Empty)}"
            };

            prop.UnderlyingTypeInfo = prop.UnderlyingType.GetTypeInfo();
            prop.Mapper = prop.UnderlyingTypeInfo.GetPropertyMapper();

            return prop;
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

        private static Property GetGroupingProperty(TypeInfo modelType, MappingAttribute attribData)
        {
            var propertyName = attribData?.GroupBy;

            if (string.IsNullOrWhiteSpace(propertyName)) return null;

            var propData = modelType.GetProperty(propertyName);

            if (propData == null) return null;

            var propAttrib = propData.GetCustomAttribute<MappingAttribute>();

            var property = new Property
            {
                PropertyName = propData.Name,
                ColumnName = propAttrib?.ColumnName ?? propData.Name,
                PropertyInfo = propData,
                UnderlyingType = propData.GetActualType()
            };

            property.UnderlyingTypeInfo = property.UnderlyingType.GetTypeInfo();
            property.Mapper = property.UnderlyingTypeInfo.GetPropertyMapper();

            return property;
        }
    }
}