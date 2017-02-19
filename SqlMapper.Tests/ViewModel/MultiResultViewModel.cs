using SqlMapper.Common;
using SqlMapper.Tests.Models;

namespace SqlMapper.Tests.ViewModel
{
    public class MultiResultViewModel
    {
        public MultiSetFirstModel MultiSetFirstModel { get; set; }

        [Mapping(ResultSetIndex = 1)]
        public MultiSetSecondModel MultiSetSecondModel { get; set; }


        public override bool Equals(object obj)
        {
            var Obj = obj as MultiResultViewModel;

            return MultiSetFirstModel.Equals(Obj?.MultiSetFirstModel)
                   && MultiSetSecondModel.Equals(Obj?.MultiSetSecondModel);
        }
    }
}