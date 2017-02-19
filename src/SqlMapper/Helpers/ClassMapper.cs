using System;
using SqlMapper.Common.Abstracts;
using SqlMapper.Common;
using SqlMapper.Extensions;
using SqlMapper.Common.Models;

namespace SqlMapper.Helpers
{
    internal class ClassMapper : IMapper
    {
        private static ClassMapper _instance;
        public static ClassMapper Instance
        {
            get
            {
                _instance = _instance ?? new ClassMapper();
                return _instance;
            }
        }

        private ClassMapper() { }

        public void Map<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            if (data.ResultSetIndex != propData.ResultSetIndex) return;

            var obj = Activator.CreateInstance(propData.UnderlyingType);

            var properties = propData.UnderlyingType.GetTypeProperties(ExcludeTypes.Select, propData);

            foreach (var property in properties)
                property.Mapper.Map(obj, data, property, entryPoint);

            propData.SetPropertyValue(model, obj);
        }

        public void MapForList<T, TMapper>(T classInstance, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint)
        {
            if (data.ResultSetIndex != propData.ResultSetIndex) return;

            var properties = propData.UnderlyingType.GetTypeProperties(ExcludeTypes.Select, propData);

            foreach (var property in properties)
                property.Mapper.Map(classInstance, data, property, entryPoint);
        }
    }
}