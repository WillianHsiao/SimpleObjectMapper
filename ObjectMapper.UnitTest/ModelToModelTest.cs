using System;
using System.Collections.Generic;
using Geo.Grid.Common.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.Common.Exception;
using ObjectMapper.UnitTest.TestModel;

namespace ObjectMapper.UnitTest
{
    [TestClass]
    public class ModelToModelTest
    {
        private static SourceModel source = new SourceModel
        {
            DecimalProp = 99999,
            DoubleProp = 88888,
            IntegerProp = 77777,
            LongProp = 66666,
            BooleanProp = false,
            StringProp = "Test",
            DateTimeProp = DateTime.Parse("2019/08/02")
        };

        private static SourceWithSubModel sourceWithSub = new SourceWithSubModel
        {
            DecimalProp = 99999,
            DoubleProp = 88888,
            IntegerProp = 77777,
            LongProp = 66666,
            BooleanProp = false,
            StringProp = "Test",
            DateTimeProp = DateTime.Parse("2019/08/02"),
            SubModel = new SubModel
            {
                SubModelString = "SubModel"
            }
        };

        [TestMethod]
        public void ModelToModel_正常情況()
        {
            var result = source.Map<TargetModel, SourceModel>();
            Assert.AreEqual(source.DecimalProp, result.DecimalProp);
            Assert.AreEqual(source.DoubleProp, result.DoubleProp);
            Assert.AreEqual(source.IntegerProp, result.IntegerProp);
            Assert.AreEqual(source.LongProp, result.LongProp);
            Assert.AreEqual(source.BooleanProp, result.BooleanProp);
            Assert.AreEqual(source.StringProp, result.StringProp);
            Assert.AreEqual(source.DateTimeProp, result.DateTimeProp);
        }

        [TestMethod]
        public void ModelToModel_有子類別_正常情況()
        {
            var result = sourceWithSub.Map<TargetModel, SourceModel>();
            Assert.AreEqual(sourceWithSub.DecimalProp, result.DecimalProp);
            Assert.AreEqual(sourceWithSub.DoubleProp, result.DoubleProp);
            Assert.AreEqual(sourceWithSub.IntegerProp, result.IntegerProp);
            Assert.AreEqual(sourceWithSub.LongProp, result.LongProp);
            Assert.AreEqual(sourceWithSub.BooleanProp, result.BooleanProp);
            Assert.AreEqual(sourceWithSub.StringProp, result.StringProp);
            Assert.AreEqual(sourceWithSub.DateTimeProp, result.DateTimeProp);
            Assert.AreEqual(sourceWithSub.SubModel, result.SubModel);
        }

        [TestMethod]
        public void ModelToModel_屬性自訂名稱()
        {
            var result = source.Map<CustomNameModel, SourceModel>();
            Assert.AreEqual(source.DecimalProp, result.CustomDecimalProp);
            Assert.AreEqual(source.DoubleProp, result.CustomDoubleProp);
            Assert.AreEqual(source.IntegerProp, result.CustomIntegerProp);
            Assert.AreEqual(source.LongProp, result.CustomLongProp);
            Assert.AreEqual(source.BooleanProp, result.CustomBooleanProp);
            Assert.AreEqual(source.StringProp, result.CustomStringProp);
        }

        [TestMethod]
        public void ModelToModel_忽略部分屬性()
        {
            var result = source.Map<IgnoreModel, SourceModel>();
            Assert.AreEqual(source.DecimalProp, result.DecimalProp);
            Assert.AreEqual(source.DoubleProp, result.DoubleProp);
            Assert.AreEqual(source.IntegerProp, result.IntegerProp);
            Assert.AreEqual(source.LongProp, result.LongProp);
            Assert.AreEqual(source.BooleanProp, result.BooleanProp);
            Assert.AreEqual(null, result.StringProp);
        }

        [TestMethod]
        public void ModelToModel_錯誤屬性名稱()
        {
            try
            {
                source.Map<WrongNameModel, SourceModel>();
            }
            catch(WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "CustomStringProp");
            }
        }

        [TestMethod]
        public void ModelToModel_混合情況()
        {
            var result = source.Map<MixModel, SourceModel>();
            Assert.AreEqual(0, result.DecimalProp);
            Assert.AreEqual(source.DoubleProp, result.CustomDoubleProp);
            Assert.AreEqual(source.IntegerProp, result.IntegerProp);
            Assert.AreEqual(0, result.CustomLongProp);
            Assert.AreEqual(source.BooleanProp, result.BooleanProp);
            Assert.AreEqual(source.StringProp, result.CustomStringProp);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ModelToModel_來源是Null()
        {
            SourceModel nullSource = null;
            nullSource.Map<TargetModel, SourceModel>();
        }

        [TestMethod]
        public void ModelToModel_陣列轉換_正常情況()
        {
            var sourceList = new List<SourceModel>();
            for (var i = 0; i < 10; i++)
            {
                sourceList.Add(source);
            }

            var result = sourceList.MapList<TargetModel, SourceModel>();
            Assert.AreEqual(sourceList.Count, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "資料來源為Null")]
        public void ModelToModel_陣列轉換_來源是Null()
        {
            List<SourceModel> sourceList = null;
            sourceList.MapList<TargetModel, SourceModel>();
        }
    }
}
