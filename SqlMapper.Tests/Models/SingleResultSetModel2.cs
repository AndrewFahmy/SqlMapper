using System;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SingleResultSetModel2
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        [Mapping(ColumnName = "CildCol1")]
        public int FirstCol { get; set; }

        [Mapping(ColumnName = "CildCol2")]
        public string SecondCol { get; set; }

        [Mapping(ColumnName = "ChildCol3")]
        public DateTime ThirdCol { get; set; }


#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var instance = obj as SingleResultSetModel2;

            return FirstCol == instance?.FirstCol
                   && SecondCol == instance.SecondCol
                   && ThirdCol == instance.ThirdCol;
        }
    }
}