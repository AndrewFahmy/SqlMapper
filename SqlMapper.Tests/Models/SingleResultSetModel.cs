using System;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
    public class SingleResultSetModel
    {
        [Mapping(ColumnName = "Col1")]
        public int FirstCol { get; set; }

        [Mapping(ColumnName = "Col2")]
        public string SecondCol { get; set; }

        [Mapping(ColumnName = "Col3")]
        public DateTime ThirdCol { get; set; }


        public override bool Equals(object obj)
        {
            var Obj = obj as SingleResultSetModel;

            return FirstCol == Obj?.FirstCol
                   && SecondCol == Obj?.SecondCol
                   && ThirdCol == Obj?.ThirdCol;
        }
    }
}