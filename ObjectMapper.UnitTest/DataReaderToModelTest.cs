using System;
using System.Collections.Generic;
using System.Data;
using Geo.Grid.Common.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.Common.Exception;
using ObjectMapper.UnitTest.Helper;
using ObjectMapper.UnitTest.TestModel;

namespace ObjectMapper.UnitTest
{
    [TestClass]
    public class DataReaderToModelTest
    {
        private static DataTable sourceDataTable = new DataTable();

        /// <summary>
        /// 每支單元測試執行前要做的事
        /// </summary>
        [TestInitialize]
        public void StartUp()
        {
            var sourceList = new List<SourceModel>
            {
                new SourceModel
                {
                    DecimalProp = 99999,
                    DoubleProp = 88888,
                    IntegerProp = 77777,
                    LongProp = 66666,
                    BooleanProp = false,
                    StringProp = "Test1"
                },
                new SourceModel
                {
                    DecimalProp = 99999,
                    DoubleProp = 88888,
                    IntegerProp = 77777,
                    LongProp = 66666,
                    BooleanProp = false,
                    StringProp = "Test1"
                },
                new SourceModel
                {
                    DecimalProp = 99999,
                    DoubleProp = 88888,
                    IntegerProp = 77777,
                    LongProp = 66666,
                    BooleanProp = false,
                    StringProp = "Test1"
                },
                new SourceModel
                {
                    DecimalProp = 99999,
                    DoubleProp = 88888,
                    IntegerProp = 77777,
                    LongProp = 66666,
                    BooleanProp = false,
                    StringProp = "Test1"
                },
                new SourceModel
                {
                    DecimalProp = 99999,
                    DoubleProp = 88888,
                    IntegerProp = 77777,
                    LongProp = 66666,
                    BooleanProp = false,
                    StringProp = "Test1"
                },
            };
            sourceDataTable = sourceList.ToDataTable();
        }

        [TestMethod]
        public void DataReader_單一值()
        {
            var reader = sourceDataTable.CreateDataReader();
            var result = reader.ToValue<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public void DataReader_單一值_錯誤欄位名稱()
        {
            var reader = sourceDataTable.CreateDataReader();
            try
            {
                reader.ToValue<decimal>("DecimalWW");
            }
            catch (WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "DecimalWW");
            }
        }

        [TestMethod]
        public void DataReader_單一值_錯誤型別()
        {
            var reader = sourceDataTable.CreateDataReader();
            try
            {
                reader.ToValue<bool>("DecimalProp");
            }
            catch (WrongTypeException ex)
            {
                Assert.IsTrue(ex.SourcePropertyType == typeof(decimal));
                Assert.IsTrue(ex.TargetPropertyType == typeof(bool));
            }
        }

        [TestMethod]
        public void DataReader_單一元素陣列()
        {
            var reader = sourceDataTable.CreateDataReader();
            var result = reader.ToList<decimal>("DecimalProp");
            Assert.AreEqual(sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public void DataReader_類別物件()
        {
            var reader = sourceDataTable.CreateDataReader();
            var result = reader.ToValue<TargetModel>();
            Assert.AreEqual(99999, result.DecimalProp);
            Assert.AreEqual(88888, result.DoubleProp);
            Assert.AreEqual(77777, result.IntegerProp);
            Assert.AreEqual(66666, result.LongProp);
            Assert.AreEqual(false, result.BooleanProp);
            Assert.AreEqual("Test1", result.StringProp);
        }

        [TestMethod]
        public void DataReader_類別物件陣列()
        {
            var reader = sourceDataTable.CreateDataReader();
            var result = reader.ToList<TargetModel>();
            Assert.AreEqual(sourceDataTable.Rows.Count, result.Count);
        }
    }
}
