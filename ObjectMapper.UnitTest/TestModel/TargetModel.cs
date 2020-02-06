using System;

namespace SimpleObjectMapper.UnitTest.TestModel
{
    public class TargetModel
    {
        public string StringProp { get; set; }
        public decimal DecimalProp { get; set; }
        public double DoubleProp { get; set; }
        public int IntegerProp { get; set; }
        public long LongProp { get; set; }
        public bool BooleanProp { get; set; }
        public DateTime DateTimeProp { get; set; }
        public SubModel SubModel { get; set; }
    }
}
