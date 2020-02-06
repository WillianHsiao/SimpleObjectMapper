using SimpleObjectMapper.Common.Attribute;

namespace SimpleObjectMapper.UnitTest.TestModel
{
    public class IgnoreModel
    {
        [MapSetting(Ignore = true)]
        public string StringProp { get; set; }
        public decimal DecimalProp { get; set; }
        public double DoubleProp { get; set; }
        public int IntegerProp { get; set; }
        public long LongProp { get; set; }
        public bool BooleanProp { get; set; }
    }
}
