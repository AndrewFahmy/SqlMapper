using System;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
    public class SingleResultSetModel2
    {
        [Mapping(ColumnName = "CildCol1")]
        public int FirstCol { get; set; }

        [Mapping(ColumnName = "CildCol2")]
        public string SecondCol { get; set; }

        [Mapping(ColumnName = "ChildCol3")]
        public DateTime ThirdCol { get; set; }


        public override bool Equals(object obj)
        {
            var Obj = obj as SingleResultSetModel2;

            return FirstCol == Obj?.FirstCol
                   && SecondCol == Obj?.SecondCol
                   && ThirdCol == Obj?.ThirdCol;
        }
    }
}