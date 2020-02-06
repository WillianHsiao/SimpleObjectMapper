using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.Common.Exception;
using ObjectMapper.UnitTest.Helper;
using ObjectMapper.UnitTest.TestModel;

namespace ObjectMapper.UnitTest
{
    [TestClass]
    public class DataReaderToModelTest
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
        public void DataReader_單一值()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = reader.MapValue<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public async Task DataReader_單一值_非同步()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = await reader.MapValueAsync<decimal>("DecimalProp");
            Assert.AreEqual(99999, result);
        }

        [TestMethod]
        public void DataReader_單一值_錯誤欄位名稱()
        {
            var reader = _sourceDataTable.CreateDataReader();
            try
            {
                reader.MapValue<decimal>("DecimalWW");
            }
            catch (WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "DecimalWW");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NameMissException))]
        public void DataReader_單一值_沒給欄位名稱()
        {
            var reader = _sourceDataTable.CreateDataReader();
            reader.MapValue<decimal>(null);
        }

        [TestMethod]
        public void DataReader_單一值_錯誤型別()
        {
            var reader = _sourceDataTable.CreateDataReader();
            try
            {
                reader.MapValue<bool>("DecimalProp");
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
            var reader = _sourceDataTable.CreateDataReader();
            var result = reader.MapValueList<decimal>("DecimalProp");
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }
        
        [TestMethod]
        public async Task DataReader_單一元素陣列_非同步()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = await reader.MapValueListAsync<decimal>("DecimalProp");
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public void DataReader_單一元素陣列_錯誤欄位名稱()
        {
            var reader = _sourceDataTable.CreateDataReader();
            try
            {
                reader.MapValueList<decimal>("DecimalWW");
            }
            catch (WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "DecimalWW");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NameMissException))]
        public void DataReader_單一元素陣列_沒給欄位名稱()
        {
            var reader = _sourceDataTable.CreateDataReader();
            reader.MapValueList<decimal>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongTypeException))]
        public void DataReader_單一元素陣列_錯誤型別()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = reader.MapValueList<DateTime>("DecimalProp");
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public void DataReader_類別物件()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = reader.MapModel<TargetModel>();
            Assert.AreEqual(99999, result.DecimalProp);
            Assert.AreEqual(88888, result.DoubleProp);
            Assert.AreEqual(77777, result.IntegerProp);
            Assert.AreEqual(66666, result.LongProp);
            Assert.AreEqual(false, result.BooleanProp);
            Assert.AreEqual("Test1", result.StringProp);
        }

        [TestMethod]
        public async Task DataReader_類別物件_非同步()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = await reader.MapModelAsync<TargetModel>();
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
            var reader = _sourceDataTable.CreateDataReader();
            var result = reader.MapModelList<TargetModel>();
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }

        [TestMethod]
        public async Task DataReader_類別物件陣列_非同步()
        {
            var reader = _sourceDataTable.CreateDataReader();
            var result = await reader.MapModelListAsync<TargetModel>();
            Assert.AreEqual(_sourceDataTable.Rows.Count, result.Count);
        }
    }
}
