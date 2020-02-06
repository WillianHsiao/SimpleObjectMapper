using SimpleObjectMapper.Common.Attribute;

namespace SimpleObjectMapper.UnitTest.TestModel
{
    public class MixModel
    {
        [MapSetting(Name = "StringProp")]
        public string CustomStringProp { get; set; }
        [MapSetting(Ignore = true)]
        public decimal DecimalProp { get; set; }
        [MapSetting(Name = "DoubleProp")]
        public double CustomDoubleProp { get; set; }
        public int IntegerProp { get; set; }
        [MapSetting(Name = "LongProp", Ignore = true)]
        public long CustomLongProp { get; set; }
        public bool BooleanProp { get; set; }
    }
}
