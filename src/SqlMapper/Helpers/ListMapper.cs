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

            var keyIndex = CheckGroupKey(data, propData, entryPoint);

            if (keyIndex > -1)
            {
                AddDataToExistingRecord(keyIndex, data, propData, entryPoint);
                entryPoint.AddToItems = false;
            }
            else AddDataToNewRecord(model, data, propData, entryPoint);
        }

        private static int CheckGroupKey<T>(CommandResult data, Property propData, IMapperMain<T> entryPoint)
        {
            var propName = propData.GroupingProperty?.ColumnName ?? propData.PrimaryKeyProperty?.ColumnName;

            if (string.IsNullOrWhiteSpace(propName)) return -1; //No Groupping or Primary Key property was found

            var val = data.Columns[propName];

            foreach (var prop in entryPoint.Groups)
                if (prop.Name == propName && prop.Value.Equals(val)) return prop.Index;

            entryPoint.Groups.Add(new GrouppingProperties
            {
                Name = propName,
                Value = val,
                Index = entryPoint.ItemsList.Count
            });

            return -1;
        }

        private static void AddDataToNewRecord<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            var underlyingType = propData.UnderlyingTypeInfo.GetGenericArguments()[0];
            var returnList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(underlyingType), 50);

            returnList.AddFromList(model, propData);

            var classInstance = Activator.CreateInstance(underlyingType);
            var classPropertyData = new Property { UnderlyingType = underlyingType, ResultSetIndex = propData.ResultSetIndex };

            ClassMapper.Instance.MapForList(classInstance, data, classPropertyData, entryPoint);

            returnList.Add(classInstance);

            propData.PropertyInfo.SetValue(model, returnList, null);
        }

        private static void AddDataToExistingRecord<TMapper>(int index, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            var underlyingType = propData.UnderlyingTypeInfo.GetGenericArguments()[0];

            var classInstance = Activator.CreateInstance(underlyingType);
            var classPropertyData = new Property { UnderlyingType = underlyingType, ResultSetIndex = propData.ResultSetIndex};

            ClassMapper.Instance.MapForList(classInstance, data, classPropertyData, entryPoint);

            var record = entryPoint.ItemsList[index];

            var listProperty = record.GetType().GetTypeInfo().GetProperty(propData.PropertyName);

            propData.UnderlyingTypeInfo.GetMethod("Add").Invoke(listProperty.GetValue(record, null), new[] { classInstance });
        }
    }
}