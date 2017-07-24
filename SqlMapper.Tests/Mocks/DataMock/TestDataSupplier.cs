
using System;
using System.Collections.Generic;
using SqlMapper.Tests.Models;
using SqlMapper.Tests.ViewModel;

namespace SqlMapper.Tests.Mocks.DataMock
{
    public class TestDataSupplier
    {
        public static Dictionary<string, object> GetSingleResultSetQueryData()
        {
            return new Dictionary<string, object>
            {
                { "Col1", "1" },
                { "Col2", "test" },
                { "Col3", DateTime.Today }
            };
        }

        public static SingleResultSetModel GetSingleResultSetMappedData()
        {
            return new SingleResultSetModel
            {
                FirstCol = 1,
                SecondCol = "test",
                ThirdCol = DateTime.Today
            };
        }

        public static List<Dictionary<string, object>> GetMultiResultSetQueryData()
        {
            return new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Col11", "11" },
                    { "Col12", "test multi" },
                    { "Col13", DateTime.Today }
                },
                new Dictionary<string, object>
                {
                    { "Col21", "21" },
                    { "Col22", "test multi 2" },
                    { "Col23", DateTime.Today }
                }
            };
        }

        public static MultiResultViewModel GetMultiResultSetMappedData()
        {
            return new MultiResultViewModel
            {
                MultiSetFirstModel = new MultiSetFirstModel
                {
                    FirstColumn = 11,
                    SecondColumn = "test multi",
                    ThirdColumn = DateTime.Today
                },
                MultiSetSecondModel = new MultiSetSecondModel
                {
                    FirstColumn = 21,
                    SecondColumn = "test multi 2",
                    ThirdColumn = DateTime.Today
                }
            };
        }

        public static List<Dictionary<string, object>> GetSingleResultSetQueryWithMultiRows()
        {
            return new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Col1", "1" },
                    { "Col2", "test" },
                    { "Col3", DateTime.Today }
                },
                new Dictionary<string, object>
                {
                    { "Col1", "2" },
                    { "Col2", "test 2" },
                    { "Col3", DateTime.Today }
                }
            };
        }

        public static List<SingleResultSetModel> GetSingleResultSetMappedWithMultiRows()
        {
            return new List<SingleResultSetModel>
            {
                new SingleResultSetModel
                {
                    FirstCol = 1,
                    SecondCol = "test",
                    ThirdCol = DateTime.Today
                },
                new SingleResultSetModel
                {
                    FirstCol = 2,
                    SecondCol = "test 2",
                    ThirdCol = DateTime.Today
                }
            };
        }

        public static List<Dictionary<string, object>> GetMultiResultSetQueryDataWithMultipleRows()
        {
            return new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Col11", "11" },
                    { "Col12", "test multi" },
                    { "Col13", DateTime.Today }
                },
                new Dictionary<string, object>
                {
                    { "Col11", "112" },
                    { "Col12", "test multi 12" },
                    { "Col13", DateTime.Today }
                },
                new Dictionary<string, object>
                {
                    { "Col21", "21" },
                    { "Col22", "test multi 2" },
                    { "Col23", DateTime.Today }
                },
                new Dictionary<string, object>
                {
                    { "Col21", "212" },
                    { "Col22", "test multi 22" },
                    { "Col23", DateTime.Today }
                }
            };
        }

        public static MultiResultViewModel2 GetMultiResultSetMappedDataWithMultiRows()
        {
            return new MultiResultViewModel2
            {
                MultiSetFirstModel = new List<MultiSetFirstModel>
                {
                    new MultiSetFirstModel
                    {
                        FirstColumn = 11,
                        SecondColumn = "test multi",
                        ThirdColumn = DateTime.Today
                    },
                    new MultiSetFirstModel
                    {
                        FirstColumn = 112,
                        SecondColumn = "test multi 12",
                        ThirdColumn = DateTime.Today
                    }
                },
                MultiSetSecondModel = new List<MultiSetSecondModel>
                {
                    new MultiSetSecondModel
                    {
                        FirstColumn = 21,
                        SecondColumn = "test multi 2",
                        ThirdColumn = DateTime.Today
                    },
                    new MultiSetSecondModel
                    {
                        FirstColumn = 212,
                        SecondColumn = "test multi 22",
                        ThirdColumn = DateTime.Today
                    }
                }
            };
        }

        public static List<Dictionary<string, object>> GetSingleResultSetWithGrouppedData()
        {
            return new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "ParentCol1", 1 },
                    { "ParentCol2", "test parent 1" },
                    { "ParentCol3", DateTime.Today },
                    { "Col1", 1 },
                    { "Col2", "test 1" },
                    { "Col3", DateTime.Today },
                    { "CildCol1", 1 },
                    { "CildCol2", "test child 1" },
                    { "CildCol3", DateTime.Today }
                },
                new Dictionary<string, object>
                {
                    { "ParentCol1", 1 },
                    { "ParentCol2", "test parent 1" },
                    { "ParentCol3", DateTime.Today },
                    { "Col1", 2 },
                    { "Col2", "test 2" },
                    { "Col3", DateTime.Today },
                    { "CildCol1", 2 },
                    { "CildCol2", "test child 2" },
                    { "CildCol3", DateTime.Today }
                }
                //new Dictionary<string, object>
                //{
                //    { "ParentCol1", 12 },
                //    { "ParentCol2", "test parent 1" },
                //    { "ParentCol3", DateTime.Today },
                //    { "Col1", 3 },
                //    { "Col2", "test 3" },
                //    { "Col3", DateTime.Today },
                //    { "CildCol1", 3 },
                //    { "CildCol2", "test child 3" },
                //    { "CildCol3", DateTime.Today }
                //}
            };
        }

        public static SingleResultSetWithGroupModel GetSingleResultSetMappedDataWithGrouppedData()
        {
            return new SingleResultSetWithGroupModel
            {
                FirstCol = 1,
                SecondCol = "test parent 1",
                ThirdCol = DateTime.Today,
                GrouppedData = new List<SingleResultSetModel>
                {
                    new SingleResultSetModel
                    {
                        FirstCol = 1,
                        SecondCol = "test 1",
                        ThirdCol = DateTime.Today
                    },
                    new SingleResultSetModel
                    {
                        FirstCol = 2,
                        SecondCol = "test 2",
                        ThirdCol = DateTime.Today
                    }
                }
            };
        }

        public static MultiLayerViewModel GetMultiLayerData()
        {
            return new MultiLayerViewModel
            {

                SingleResultSetWithGroupModels = new List<SingleResultSetWithGroupModel>
                {
                    new SingleResultSetWithGroupModel
                    {
                        FirstCol = 1,
                        SecondCol = "test parent 1",
                        ThirdCol = DateTime.Today,
                        GrouppedData = new List<SingleResultSetModel>
                        {
                            new SingleResultSetModel
                            {
                                FirstCol = 1,
                                SecondCol = "test 1",
                                ThirdCol = DateTime.Today
                            },
                            new SingleResultSetModel
                            {
                                FirstCol = 2,
                                SecondCol = "test 2",
                                ThirdCol = DateTime.Today
                            }
                        }
                    },
                    new SingleResultSetWithGroupModel
                    {
                        FirstCol = 2,
                        SecondCol = "test parent 2",
                        ThirdCol = DateTime.Today,
                        GrouppedData = new List<SingleResultSetModel>
                        {
                            new SingleResultSetModel
                            {
                                FirstCol = 3,
                                SecondCol = "test 3",
                                ThirdCol = DateTime.Today
                            },
                            new SingleResultSetModel
                            {
                                FirstCol = 4,
                                SecondCol = "test 4",
                                ThirdCol = DateTime.Today
                            }
                        }
                    }
                }
            };
        }
    }
}