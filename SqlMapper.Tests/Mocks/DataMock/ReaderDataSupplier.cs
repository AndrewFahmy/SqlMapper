using System;
using System.Collections.Generic;
using SqlMapper.Tests.Mocks.Models;

namespace SqlMapper.Tests.Mocks.DataMock
{
    public class ReaderDataSupplier
    {
        public ResultSetModel GetSingleResultSetQueryData()
        {
            return new ResultSetModel
            {
                Columns = new List<string>
                {
                    "Col1",
                    "Col2",
                    "Col3"
                },
                Index = 0,
                Rows = new List<RowModel>
                {
                    new RowModel
                    {
                        ColumnValues = new List<object>
                        {
                            "1",
                            "test",
                            DateTime.Today
                        }
                    }
                }
            };
        }

        public List<ResultSetModel> GetMultiResultSetQueryData()
        {
            return new List<ResultSetModel>
            {
                new ResultSetModel
                {
                    Columns = new List<string>
                    {
                        "Col11",
                        "Col12",
                        "Col13"
                    },
                    Index = 0,
                    Rows = new List<RowModel>
                    {
                        new RowModel
                        {
                            ColumnValues = new List<object>
                            {
                                "11",
                                "test multi",
                                DateTime.Today
                            }
                        }
                    }
                },
                new ResultSetModel
                {
                    Columns = new List<string>
                    {
                        "Col21",
                        "Col22",
                        "Col23"
                    },
                    Index = 0,
                    Rows = new List<RowModel>
                    {
                        new RowModel
                        {
                            ColumnValues = new List<object>
                            {
                                "21",
                                "test multi 2",
                                DateTime.Today
                            }
                        }
                    }
                }
            };
        }

        public ResultSetModel GetSingleResultSetQueryWithMultiRows()
        {
            return new ResultSetModel
            {
                Columns = new List<string>
                {
                    "Col1",
                    "Col2",
                    "Col3"
                },
                Index = 0,
                Rows = new List<RowModel>
                {
                    new RowModel
                    {
                        ColumnValues = new List<object>
                        {
                            "1",
                            "test",
                            DateTime.Today
                        }
                    },
                    new RowModel
                    {
                       ColumnValues = new List<object>
                        {
                            "2",
                            "test 2",
                            DateTime.Today
                        }
                    }
                }
            };
        }

        public List<ResultSetModel> GetMultiResultSetWithMultiRows()
        {
            return new List<ResultSetModel>
            {
                new ResultSetModel
                {
                    Columns = new List<string>
                    {
                        "Col11",
                        "Col12",
                        "Col13"
                    },
                    Index = 0,
                    Rows = new List<RowModel>
                    {
                        new RowModel
                        {
                            ColumnValues = new List<object>
                            {
                                "11",
                                "test multi",
                                DateTime.Today
                            }
                        },
                        new RowModel
                        {
                            ColumnValues = new List<object>
                            {
                                "112",
                                "test multi 12",
                                DateTime.Today
                            }
                        }
                    }
                },
                new ResultSetModel
                {
                    Columns = new List<string>
                    {
                        "Col21",
                        "Col22",
                        "Col23"
                    },
                    Index = 0,
                    Rows = new List<RowModel>
                    {
                        new RowModel
                        {
                            ColumnValues = new List<object>
                            {
                                "21",
                                "test multi 2",
                                DateTime.Today
                            }
                        },
                        new RowModel
                        {
                            ColumnValues = new List<object>
                            {
                                "212",
                                "test multi 22",
                                DateTime.Today
                            }
                        }
                    }
                }
            };
        }

        public ResultSetModel GetSingleResultSetGroupped()
        {
            return new ResultSetModel
            {
                Columns = new List<string>
                {
                    "ParentCol1",
                    "ParentCol2",
                    "ParentCol3",
                    "Col1",
                    "Col2",
                    "Col3",
                    "CildCol1",
                    "CildCol2",
                    "CildCol3"
                },
                Index = 0,
                Rows = new List<RowModel>
                {
                    new RowModel
                    {
                        ColumnValues = new List<object>
                        {
                            1,
                            "test parent 1",
                            DateTime.Today,
                            1,
                            "test 1",
                            DateTime.Today,
                            1,
                            "test child 1",
                            DateTime.Today
                        }
                    },
                    new RowModel
                    {
                        ColumnValues = new List<object>
                        {
                            1,
                            "test parent 1",
                            DateTime.Today,
                            2,
                            "test 2",
                            DateTime.Today,
                            2,
                            "test child 2",
                            DateTime.Today
                        }
                    }
                    //new RowModel
                    //{
                    //    ColumnValues = new List<object>
                    //    {
                    //        12,
                    //        "test parent 1",
                    //        DateTime.Today,
                    //        3,
                    //        "test 3",
                    //        DateTime.Today,
                    //        3,
                    //        "test child 3",
                    //        DateTime.Today
                    //    }
                    //}
                }
            };
        }
    }
}