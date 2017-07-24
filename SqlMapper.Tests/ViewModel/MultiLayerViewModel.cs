using System.Collections.Generic;
using SqlMapper.Common;
using SqlMapper.Tests.Models;

namespace SqlMapper.Tests.ViewModel
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class MultiLayerViewModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        [Mapping(GroupBy = "ParentCol1")]
        public List<SingleResultSetWithGroupModel> SingleResultSetWithGroupModels { get; set; }


#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var instance = obj as MultiLayerViewModel;

            var result = true;

            for (var i = 0; i < SingleResultSetWithGroupModels.Count; i++)
                result = result && SingleResultSetWithGroupModels[i]
                             .Equals(instance?.SingleResultSetWithGroupModels[i]);
            return result;
        }
    }
}