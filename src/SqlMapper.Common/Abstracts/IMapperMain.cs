using System.Collections.Generic;
using SqlMapper.Common.Models;

namespace SqlMapper.Common.Abstracts
{
    internal interface IMapperMain<T>
    {
        IList<GrouppingProperties> Groups { get; set; }
        IList<T> ItemsList { get; set; }
        bool AddToItems { get; set; }

        void Map(CommandResult data, T instance, IList<Property> typeProperties);
    }
}