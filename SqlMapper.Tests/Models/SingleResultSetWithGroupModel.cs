using System;
using System.Collections.Generic;
using SqlMapper.Common;

namespace SqlMapper.Tests.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SingleResultSetWithGroupModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        [PrimaryKey]
        [Mapping(ColumnName = "ParentCol1")]
        public int FirstCol { get; set; }

        [Mapping(ColumnName = "ParentCol2")]
        public string SecondCol { get; set; }

        [Mapping(ColumnName = "ParentCol3")]
        public DateTime ThirdCol { get; set; }


        public List<SingleResultSetModel> GrouppedData { get; set; }


#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var instance = obj as SingleResultSetWithGroupModel;

            bool result;

            result = FirstCol == instance?.FirstCol
                            && SecondCol == instance.SecondCol
                            && ThirdCol == instance.ThirdCol;

            for (var i = 0; i < GrouppedData.Count; i++)
                result = result && GrouppedData[i].Equals(instance.GrouppedData[i]);

            //for (var j = 0; j < GrouppedData2.Count; j++)
            //    result = result && GrouppedData2[j].Equals(Obj?.GrouppedData2[j]);

            return result;
        }
    }
}