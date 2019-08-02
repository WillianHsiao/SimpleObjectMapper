﻿using System;
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
            DateTimeProp = DateTime.Parse("2019/08/02"),
            SubModel = new SubModel()
            {
                SubModelString = "SubString"
            }
        };

        [TestMethod]
        public void ModelToModel_正常情況()
        {
            var result = source.Map<TargetModel>();
            Assert.AreEqual(source.DecimalProp, result.DecimalProp);
            Assert.AreEqual(source.DoubleProp, result.DoubleProp);
            Assert.AreEqual(source.IntegerProp, result.IntegerProp);
            Assert.AreEqual(source.LongProp, result.LongProp);
            Assert.AreEqual(source.BooleanProp, result.BooleanProp);
            Assert.AreEqual(source.StringProp, result.StringProp);
            Assert.AreEqual(source.DateTimeProp, result.DateTimeProp);
            Assert.AreEqual(source.SubModel, result.SubModel);
        }

        [TestMethod]
        public void ModelToModel_屬性自訂名稱()
        {
            var result = source.Map<CustomNameModel>();
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
            var result = source.Map<IgnoreModel>();
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
                source.Map<WrongNameModel>();
            }
            catch(WrongNameException ex)
            {
                Assert.IsTrue(ex.TargetPropertyName == "CustomStringProp");
            }
        }

        [TestMethod]
        public void ModelToModel_混合情況()
        {
            var result = source.Map<MixModel>();
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
            nullSource.Map<TargetModel>();
        }
    }
}
