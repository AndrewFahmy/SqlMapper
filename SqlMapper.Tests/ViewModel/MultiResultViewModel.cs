using SqlMapper.Common;
using SqlMapper.Tests.Models;

namespace SqlMapper.Tests.ViewModel
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class MultiResultViewModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public MultiSetFirstModel MultiSetFirstModel { get; set; }

        [Mapping(ResultSetIndex = 1)]
        public MultiSetSecondModel MultiSetSecondModel { get; set; }


#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var instance = obj as MultiResultViewModel;

            return MultiSetFirstModel.Equals(instance?.MultiSetFirstModel)
                   && MultiSetSecondModel.Equals(instance?.MultiSetSecondModel);
        }
    }
}