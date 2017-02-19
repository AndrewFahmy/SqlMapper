using System.Collections.Generic;

namespace SqlMapper.Tests.Mocks.Models
{
    public class ResultSetModel
    {
        public int Index { get; set; }

        public List<string> Columns { get; set; }

        public List<RowModel> Rows { get; set; }
    }
}