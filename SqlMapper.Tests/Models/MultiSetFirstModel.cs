using System;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
    public class MultiSetFirstModel
    {
        [Mapping(ColumnName = "Col11")]
        public int FirstColumn { get; set; }

        [Mapping(ColumnName = "Col12")]
        public string SecondColumn { get; set; }

        [Mapping(ColumnName = "Col13")]
        public DateTime ThirdColumn { get; set; }


        public override bool Equals(object obj)
        {
            var Obj = obj as MultiSetFirstModel;

            return FirstColumn == Obj?.FirstColumn
                   && SecondColumn == Obj?.SecondColumn
                   && ThirdColumn == Obj?.ThirdColumn;
        }
    }
}