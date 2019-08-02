using System;
using Geo.Grid.Common.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.Common.Exception;

namespace ObjectMapper.UnitTest
{
    [TestClass]
    public class ValueToValueTest
    {
        [TestMethod]
        public void ValueToValue_Decimal_正常()
        {
            decimal source = 99999;
            var result = source.Map<decimal>();
            Assert.AreEqual(source, result);
        }

        [TestMethod]
        public void ValueToValue_DecimalToInt_正常()
        {
            decimal source = 99999;
            var result = source.Map<int>();
            Assert.AreEqual(source, result);
        }

        [TestMethod]
        public void ValueToValue_DateTime_正常()
        {
            var source = DateTime.Parse("2019/01/01");
            var result = source.Map<DateTime>();
            Assert.AreEqual(source, result);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongTypeException))]
        public void ValueToValue_DateTime_錯誤型別()
        {
            var source = DateTime.Now;
            source.Map<bool>();
        }
    }
}
