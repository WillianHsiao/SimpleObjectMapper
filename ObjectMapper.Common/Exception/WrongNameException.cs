namespace ObjectMapper.Common.Exception
{
    /// <summary>
    /// 名稱設定錯誤例外
    /// </summary>
    public class WrongNameException : System.Exception
    {
        public WrongNameException() : base() { }

        public WrongNameException(string message) : base(message) { }

        /// <summary>
        /// 目標型別名稱
        /// </summary>
        public string TargetPropertyName { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public override string Message
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.Message))
                {
                    return base.Message;
                }

                return "找不到該名稱對應";
            }
        }
    }
}
