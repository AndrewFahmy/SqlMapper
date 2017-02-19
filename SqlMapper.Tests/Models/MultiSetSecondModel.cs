using System;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
    public class MultiSetSecondModel
    {
        [Mapping(ColumnName = "Col21")]
        public int FirstColumn { get; set; }

        [Mapping(ColumnName = "Col22")]
        public string SecondColumn { get; set; }

        [Mapping(ColumnName = "Col23")]
        public DateTime ThirdColumn { get; set; }


        public override bool Equals(object obj)
        {
            var Obj = obj as MultiSetSecondModel;

            return FirstColumn == Obj?.FirstColumn
                   && SecondColumn == Obj?.SecondColumn
                   && ThirdColumn == Obj?.ThirdColumn;
        }
    }
}