namespace SimpleObjectMapper.Common.Attribute
{
    public class MapSettingAttribute : System.Attribute
    {
        /// <summary>
        /// 欄位/屬性名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool Ignore { get; set; }
    }
}
