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
            var result = dataRow.ToValue<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public void DataTable_單一值_錯誤欄位名稱()
        {
            var dataRow = _sourceDataTable.Rows[0];
            try
            {
                dataRow.ToValue<decimal>("DecimalPropWW");
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
                dataRow.ToValue<bool>("DecimalProp");
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
            var result = _sourceDataTable.ToValueList<decimal>("DecimalProp");
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public void DataTable_類別物件()
        {
            var dataRow = _sourceDataTable.Rows[0];
            var result = dataRow.ToModel<TargetModel>();
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
            var result = _sourceDataTable.ToModelList<TargetModel>();
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }
    }
}
