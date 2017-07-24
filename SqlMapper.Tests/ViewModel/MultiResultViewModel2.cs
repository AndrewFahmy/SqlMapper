using System.Collections.Generic;
using SqlMapper.Common;
using SqlMapper.Tests.Models;

namespace SqlMapper.Tests.ViewModel
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class MultiResultViewModel2
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        [Mapping(GroupBy = "Col11")]
        public List<MultiSetFirstModel> MultiSetFirstModel { get; set; }

        [Mapping(ResultSetIndex = 1, GroupBy = "Col21")]
        public List<MultiSetSecondModel> MultiSetSecondModel { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var instance = obj as MultiResultViewModel2;

            var result = true;

            for (var i = 0; i < MultiSetFirstModel.Count; i++)
                result = result && MultiSetFirstModel[i].Equals(instance?.MultiSetFirstModel[i]);

            for (var j = 0; j < MultiSetSecondModel.Count; j++)
                result = result && MultiSetSecondModel[j].Equals(instance?.MultiSetSecondModel[j]);


            return result;
        }
    }
}