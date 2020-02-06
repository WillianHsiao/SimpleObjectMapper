using SimpleObjectMapper.Common.Attribute;

namespace SimpleObjectMapper.UnitTest.TestModel
{
    public class WrongNameModel
    {
        [MapSetting(Name = "WrongStringProp")]
        public string CustomStringProp { get; set; }
        [MapSetting(Name = "WrongDecimalProp")]
        public decimal CustomDecimalProp { get; set; }
        [MapSetting(Name = "DoubleProp")]
        public double CustomDoubleProp { get; set; }
        [MapSetting(Name = "IntegerProp")]
        public int CustomIntegerProp { get; set; }
        [MapSetting(Name = "LongProp")]
        public long CustomLongProp { get; set; }
        [MapSetting(Name = "BooleanProp")]
        public bool CustomBooleanProp { get; set; }
    }
}
