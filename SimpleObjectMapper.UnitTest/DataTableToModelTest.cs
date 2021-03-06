﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleObjectMapper.Common.Exception;
using SimpleObjectMapper.UnitTest.Helper;
using SimpleObjectMapper.UnitTest.TestModel;

namespace SimpleObjectMapper.UnitTest
{
    [TestClass]
    public class DataTableToModelTest
    {
        private static DataTable _sourceDataTable = new DataTable();

        /// <summary>
        /// 每支單元測試執行前要做的事
        /// </summary>
        [TestInitialize]
        public void StartUp()
        {
            var sourceList = new List<SourceModel>();
            for (var i = 0; i < 10; i++)
            {
                sourceList.Add(new SourceModel
                {
                    DecimalProp = 99999,
                    DoubleProp = 88888,
                    IntegerProp = 77777,
                    LongProp = 66666,
                    BooleanProp = false,
                    StringProp = "Test1"
                });
            }
            _sourceDataTable = sourceList.ToDataTable();
        }

        [TestMethod]
        public void DataTable_單一值()
        {
            var dataRow = _sourceDataTable.Rows[0];
            var result = dataRow.MapValue<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public async Task DataTable_單一值_非同步()
        {
            var dataRow = _sourceDataTable.Rows[0];
            var result = await dataRow.MapValueAsync<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public void DataTable_單一值_錯誤欄位名稱()
        {
            var dataRow = _sourceDataTable.Rows[0];
            try
            {
                dataRow.MapValue<decimal>("DecimalPropWW");
            }
            catch(WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "DecimalPropWW");
            }
        }

        [TestMethod]
        public void DataTable_單一值_錯誤型別()
        {
            var dataRow = _sourceDataTable.Rows[0];
            try
            {
                dataRow.MapValue<bool>("DecimalProp");
            }
            catch (WrongTypeException ex)
            {
                Assert.IsTrue(ex.SourcePropertyType == typeof(decimal));
                Assert.IsTrue(ex.TargetPropertyType == typeof(bool));
            }
        }

        [TestMethod]
        public void DataTable_單一元素陣列()
        {
            var result = _sourceDataTable.MapValueList<decimal>("DecimalProp");
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public void DataTable_類別物件()
        {
            var dataRow = _sourceDataTable.Rows[0];
            var result = dataRow.MapModel<TargetModel>();
            Assert.AreEqual(99999, result.DecimalProp);
            Assert.AreEqual(88888, result.DoubleProp);
            Assert.AreEqual(77777, result.IntegerProp);
            Assert.AreEqual(66666, result.LongProp);
            Assert.AreEqual(false, result.BooleanProp);
            Assert.AreEqual("Test1", result.StringProp);
        }

        [TestMethod]
        public void DataTable_類別物件陣列()
        {
            var result = _sourceDataTable.MapModelList<TargetModel>();
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }
    }
}
