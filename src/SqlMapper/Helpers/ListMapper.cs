using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SqlMapper.Common.Abstracts;
using SqlMapper.Extensions;
using SqlMapper.Common.Models;

namespace SqlMapper.Helpers
{
    internal class ListMapper : IMapper
    {
        private static ListMapper _instance;
        public static ListMapper Instance
        {
            get
            {
                _instance = _instance ?? new ListMapper();
                return _instance;
            }
        }

        private ListMapper() { }

        public void Map<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            if (data.ResultSetIndex != propData.ResultSetIndex) return;

            var instance = CheckGroupKey(model, data, propData, entryPoint);

            if (instance != null)
            {
                AddDataToExistingRecord(instance, data, propData, entryPoint);
                entryPoint.AddToItems = false;
                // ReSharper disable once RedundantAssignment
                model = (T)instance;
            }
            else AddDataToNewRecord(model, data, propData, entryPoint);
        }

        private static object CheckGroupKey<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            var propName = propData.GroupingProperty?.ColumnName ?? propData.PrimaryKeyProperty?.ColumnName;

            if (string.IsNullOrWhiteSpace(propName)) return null; //No Groupping or Primary Key property was found

            var val = data.Columns[propName];

            foreach (var prop in entryPoint.Groups)
                if (prop.Name == propName && prop.Value.Equals(val)) return prop.Instance;

            entryPoint.Groups.Add(new GrouppingProperties
            {
                Name = propName,
                Value = val,
                Instance = model
            });

            return null;
        }

        private static void AddDataToNewRecord<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            var underlyingType = propData.UnderlyingTypeInfo.GetGenericArguments()[0];
            var returnList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(underlyingType), 50);

            returnList.AddFromList(model, propData);

            var classInstance = Activator.CreateInstance(underlyingType);
            var classPropertyData = new Property { UnderlyingType = underlyingType, ResultSetIndex = propData.ResultSetIndex };

            ClassMapper.Instance.MapForList(classInstance, data, classPropertyData, entryPoint);

            if (entryPoint.AddToItems)
                returnList.Add(classInstance);

            propData.PropertyInfo.SetMethod.Invoke(model, new object[] { returnList });
        }

        private static void AddDataToExistingRecord<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            var underlyingType = propData.UnderlyingTypeInfo.GetGenericArguments()[0];

            var classInstance = Activator.CreateInstance(underlyingType);
            var classPropertyData = new Property { UnderlyingType = underlyingType, ResultSetIndex = propData.ResultSetIndex };

            ClassMapper.Instance.MapForList(classInstance, data, classPropertyData, entryPoint);

            var listProperty = model.GetType().GetTypeInfo().GetProperty(propData.PropertyName);

            if (listProperty != null)
                propData.UnderlyingTypeInfo.GetMethod("Add").Invoke(listProperty.GetValue(model, null), new[] { classInstance });
        }
    }
}