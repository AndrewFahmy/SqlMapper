using System.Collections.Generic;
using SqlMapper.Common;
using SqlMapper.Tests.Models;

namespace SqlMapper.Tests.ViewModel
{
    public class MultiResultViewModel2
    {
        [Mapping(GroupBy = "Col11")]
        public List<MultiSetFirstModel> MultiSetFirstModel { get; set; }

        [Mapping(ResultSetIndex = 1, GroupBy = "Col21")]
        public List<MultiSetSecondModel> MultiSetSecondModel { get; set; }

        public override bool Equals(object obj)
        {
            var Obj = obj as MultiResultViewModel2;

            var result = true;

            for (var i = 0; i < MultiSetFirstModel.Count; i++)
                result = result && MultiSetFirstModel[i].Equals(Obj?.MultiSetFirstModel[i]);

            for (var j = 0; j < MultiSetSecondModel.Count; j++)
                result = result && MultiSetSecondModel[j].Equals(Obj?.MultiSetSecondModel[j]);


            return result;
        }
    }
}