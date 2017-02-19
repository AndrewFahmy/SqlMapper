using System;
using System.Collections.Generic;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
    public class SingleResultSetWithGroupModel
    {
        [PrimaryKey]
        [Mapping(ColumnName = "ParentCol1")]
        public int FirstCol { get; set; }

        [Mapping(ColumnName = "ParentCol2")]
        public string SecondCol { get; set; }

        [Mapping(ColumnName = "ParentCol3")]
        public DateTime ThirdCol { get; set; }


        public List<SingleResultSetModel> GrouppedData { get; set; }


        public override bool Equals(object obj)
        {
            var Obj = obj as SingleResultSetWithGroupModel;

            var result = true;

            result = result && FirstCol == Obj?.FirstCol
                            && SecondCol == Obj?.SecondCol
                            && ThirdCol == Obj?.ThirdCol;

            for (var i = 0; i < GrouppedData.Count; i++)
                result = result && GrouppedData[i].Equals(Obj?.GrouppedData[i]);

            //for (var j = 0; j < GrouppedData2.Count; j++)
            //    result = result && GrouppedData2[j].Equals(Obj?.GrouppedData2[j]);

            return result;
        }
    }
}