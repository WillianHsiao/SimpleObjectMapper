using System;
using System.Collections.Generic;
using System.Data;
using Geo.Grid.Common.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.UnitTest.Helper;
using ObjectMapper.UnitTest.TestModel;

namespace ObjectMapper.UnitTest
{
    [TestClass]
    public class VsOldWayTest
    {
        private static SourceModel _source = new SourceModel
        {
            DecimalProp = 99999,
            DoubleProp = 88888,
            IntegerProp = 77777,
            LongProp = 66666,
            BooleanProp = false,
            StringProp = "Test",
            DateTimeProp = DateTime.Parse("2019/08/02")
        };

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
                sourceList.Add(_source);
            }
            _sourceDataTable = sourceList.ToDataTable();
        }

        [TestMethod]
        public void ModelToModel()
        {
            //Old Way
            var targetOld = new TargetModel();
            targetOld.StringProp = _source.StringProp;
            targetOld.DecimalProp = _source.DecimalProp;
            targetOld.DoubleProp = _source.DoubleProp;
            targetOld.IntegerProp = _source.IntegerProp;
            targetOld.LongProp = _source.LongProp;
            targetOld.BooleanProp = _source.BooleanProp;
            targetOld.DateTimeProp = _source.DateTimeProp;

            //New Way
            var targetNew = _source.Map<TargetModel>();

            Assert.AreEqual(targetOld.StringProp, targetNew.StringProp);
            Assert.AreEqual(targetOld.DecimalProp, targetNew.DecimalProp);
            Assert.AreEqual(targetOld.DoubleProp, targetNew.DoubleProp);
            Assert.AreEqual(targetOld.IntegerProp, targetNew.IntegerProp);
            Assert.AreEqual(targetOld.LongProp, targetNew.LongProp);
            Assert.AreEqual(targetOld.BooleanProp, targetNew.BooleanProp);
            Assert.AreEqual(targetOld.DateTimeProp, targetNew.DateTimeProp);
        }

        [TestMethod]
        public void DataTableToSingleValueList()
        {
            //Old Way
            var decimalsOld = new List<decimal>();
            foreach (DataRow dataRow in _sourceDataTable.Rows)
            {
                decimalsOld.Add(Convert.ToDecimal(dataRow["DecimalProp"]));
            }
            //New Way One
            var decimalsNew1 = new List<decimal>();
            foreach (DataRow dataRow in _sourceDataTable.Rows)
            {
                decimalsNew1.Add(dataRow.MapValue<decimal>("DecimalProp"));
            }
            //New Way Two
            var decimalsNew2 = _sourceDataTable.MapValueList<decimal>("DecimalProp");

            Assert.AreEqual(decimalsOld.Count, decimalsNew1.Count);
            Assert.AreEqual(decimalsNew1.Count, decimalsNew2.Count);
        }

        public void DataTableToModelList()
        {

        }
    }
}
