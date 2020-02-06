namespace ObjectMapper.UnitTest.TestModel
{
    public class SourceWithSubModel : SourceModel
    {
        //[MapSetting(Ignore = true)]
        public SubModel SubModel { get; set; }
    }
}
