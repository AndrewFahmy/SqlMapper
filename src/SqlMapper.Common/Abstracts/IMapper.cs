using SqlMapper.Common.Models;

namespace SqlMapper.Common.Abstracts
{
    internal interface IMapper
    {
        void Map<T, TMapper>(T model, CommandResult data, Property propData, IMapperMain<TMapper> entryPoint);
    }
}