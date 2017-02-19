using SqlMapper.Common.Models;
using System.Collections.Generic;
using SqlMapper.Common.Abstracts;

namespace SqlMapper.Helpers
{
    internal class MapperMain<T> : IMapperMain<T>
    {
        public IList<GrouppingProperties> Groups { get; set; }
        public IList<T> ItemsList { get; set; }
        public bool AddToItems { get; set; }

        internal MapperMain()
        {
            Groups = new List<GrouppingProperties>();
            ItemsList = new List<T>();
        }

        public void Map(CommandResult data, T instance, IList<Property> typeProperties)
        {
            AddToItems = true;

            foreach (var property in typeProperties)
                property.Mapper.Map(instance, data, property, this);

            if (AddToItems)
                ItemsList.Add(instance);
        }
    }
}