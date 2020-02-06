using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleObjectMapper.UnitTest.Helper;
using SimpleObjectMapper.UnitTest.TestModel;

namespace SimpleObjectMapper.UnitTest
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

        private static List<SourceModel> _sourceList = new List<SourceModel>();

        private static DataTable _sourceDataTable = new DataTable();

        /// <summary>
        /// 每支單元測試執行前要做的事
        /// </summary>
        [TestInitialize]
        public void StartUp()
        {
            for (var i = 0; i < 10; i++)
            {
                _sourceList.Add(_source);
            }
            _sourceDataTable = _sourceList.ToDataTable();
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
            var targetNew = _source.Map<TargetModel, SourceModel>();

            //Result
            Assert.AreEqual(targetOld.StringProp, targetNew.StringProp);
            Assert.AreEqual(targetOld.DecimalProp, targetNew.DecimalProp);
            Assert.AreEqual(targetOld.DoubleProp, targetNew.DoubleProp);
            Assert.AreEqual(targetOld.IntegerProp, targetNew.IntegerProp);
            Assert.AreEqual(targetOld.LongProp, targetNew.LongProp);
            Assert.AreEqual(targetOld.BooleanProp, targetNew.BooleanProp);
            Assert.AreEqual(targetOld.DateTimeProp, targetNew.DateTimeProp);
        }

        [TestMethod]
        public void ModelListToModelList()
        {
            //Old Way 1
            var targetOldList1 = new List<TargetModel>();
            foreach (var source in _sourceList)
            {
                var target = new TargetModel();

                target.StringProp = source.StringProp;
                target.DecimalProp = source.DecimalProp;
                target.DoubleProp = source.DoubleProp;
                target.IntegerProp = source.IntegerProp;
                target.LongProp = source.LongProp;
                target.BooleanProp = source.BooleanProp;
                target.DateTimeProp = source.DateTimeProp;
                targetOldList1.Add(target);
            }
            //Old Way 2
            var targetOldList2 = _sourceList.Select(s => new TargetModel
            {
                StringProp = s.StringProp,
                DecimalProp = s.DecimalProp,
                DoubleProp = s.DoubleProp,
                IntegerProp = s.IntegerProp,
                LongProp = s.LongProp,
                BooleanProp = s.BooleanProp,
                DateTimeProp = s.DateTimeProp
            }).ToList();

            //New Way
            var targetNewList = _sourceList.MapList<TargetModel, SourceModel>();

            //Result
            Assert.AreEqual(targetOldList1.Count, targetOldList2.Count);
            Assert.AreEqual(targetOldList1.Count, targetNewList.Count);
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

            //Result
            Assert.AreEqual(decimalsOld.Count, decimalsNew1.Count);
            Assert.AreEqual(decimalsOld.Count, decimalsNew2.Count);
        }

        [TestMethod]
        public void DataTableToModelList()
        {
            //Old Way
            var modelListOld = new List<TargetModel>();
            foreach (DataRow dataRow in _sourceDataTable.Rows)
            {
                var model = new TargetModel();
                model.StringProp = dataRow["StringProp"].ToString();
                model.DecimalProp = Convert.ToDecimal(dataRow["DecimalProp"]);
                model.DoubleProp = Convert.ToDouble(dataRow["DoubleProp"]);
                model.IntegerProp = Convert.ToInt32(dataRow["IntegerProp"]);
                model.LongProp = Convert.ToInt64(dataRow["LongProp"]);
                model.BooleanProp = Convert.ToBoolean(dataRow["BooleanProp"]);
                model.DateTimeProp = Convert.ToDateTime(dataRow["DateTimeProp"]);
                modelListOld.Add(model);
            }

            //New Way One
            var modelListNew1 = new List<TargetModel>();
            foreach (DataRow dataRow in _sourceDataTable.Rows)
            {
                var model = dataRow.MapModel<TargetModel>();
                modelListNew1.Add(model);
            }

            //New Way Two
            var modelListNew2 = _sourceDataTable.MapModelList<TargetModel>();

            //Result
            Assert.AreEqual(modelListOld.Count, modelListNew1.Count);
            Assert.AreEqual(modelListOld.Count, modelListNew2.Count);
        }

        [TestMethod]
        public void DataReaderToSingleValueList()
        {
            //Old Way
            var decimalsOld = new List<decimal>();
            var dataReaderOld = _sourceDataTable.CreateDataReader();
            while (dataReaderOld.Read())
            {
                decimalsOld.Add(Convert.ToDecimal(dataReaderOld["DecimalProp"]));
            }

            //New Way
            var dataReaderNew = _sourceDataTable.CreateDataReader();
            var decimalsNew = dataReaderNew.MapValueList<decimal>("DecimalProp");

            //Result
            Assert.AreEqual(decimalsOld.Count, decimalsNew.Count);
        }

        [TestMethod]
        public void DataReaderToModelList()
        {
            //Old Way
            var modelListOld = new List<TargetModel>();
            var dataReaderOld = _sourceDataTable.CreateDataReader();
            while(dataReaderOld.Read())
            {
                var model = new TargetModel();
                model.StringProp = dataReaderOld["StringProp"].ToString();
                model.DecimalProp = Convert.ToDecimal(dataReaderOld["DecimalProp"]);
                model.DoubleProp = Convert.ToDouble(dataReaderOld["DoubleProp"]);
                model.IntegerProp = Convert.ToInt32(dataReaderOld["IntegerProp"]);
                model.LongProp = Convert.ToInt64(dataReaderOld["LongProp"]);
                model.BooleanProp = Convert.ToBoolean(dataReaderOld["BooleanProp"]);
                model.DateTimeProp = Convert.ToDateTime(dataReaderOld["DateTimeProp"]);
                modelListOld.Add(model);
            }

            //New Way
            var dataReaderNew = _sourceDataTable.CreateDataReader();
            var modelListNew = dataReaderNew.MapModelList<TargetModel>();

            //Result
            Assert.AreEqual(modelListOld.Count, modelListNew.Count);
        }
    }
}
