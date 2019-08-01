using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Geo.Grid.Common.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.Common.Exception;
using ObjectMapper.UnitTest.Helper;
using ObjectMapper.UnitTest.TestModel;

namespace ObjectMapper.UnitTest
{
    [TestClass]
    public class AdoNetToModelTest
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
        public void AdoNet_單一值()
        {
            var dataRow = sourceDataTable.Rows[0];
            var result = dataRow.ToSingleValue<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public void AdoNet_單一值_錯誤欄位名稱()
        {
            var dataRow = sourceDataTable.Rows[0];
            try
            {
                dataRow.ToSingleValue<decimal>("DecimalPropWW");
            }
            catch(WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "DecimalPropWW");
            }
        }

        public void AdoNet_單一值_錯誤型別()
        {
            var dataRow = sourceDataTable.Rows[0];
            try
            {
                dataRow.ToSingleValue<bool>("DecimalProp");
            }
            catch (WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "DecimalPropWW");
            }
        }

        [TestMethod]
        public void AdoNet_單一元素陣列()
        {
            var result = sourceDataTable.ToList<decimal>("DecimalProp");
            Assert.AreEqual(sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public void AdoNet_類別物件()
        {
            var dataRow = sourceDataTable.Rows[0];
            var result = dataRow.ToModel<SourceModel>();
            Assert.AreEqual(99999, result.DecimalProp);
            Assert.AreEqual(88888, result.DoubleProp);
            Assert.AreEqual(77777, result.IntegerProp);
            Assert.AreEqual(66666, result.LongProp);
            Assert.AreEqual(false, result.BooleanProp);
            Assert.AreEqual("Test1", result.StringProp);
        }

        [TestMethod]
        public void AdoNet_類別物件陣列()
        {
            var result = sourceDataTable.ToList<SourceModel>();
            Assert.AreEqual(sourceDataTable.Rows.Count, result.Count);
        }
    }
}
