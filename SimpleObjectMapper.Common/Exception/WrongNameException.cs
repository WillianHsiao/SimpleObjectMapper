namespace SimpleObjectMapper.Common.Exception
{
    /// <summary>
    /// 名稱設定錯誤例外
    /// </summary>
    public class WrongNameException : System.Exception
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public WrongNameException() { }

        /// <summary>
        /// 建構子自訂訊息
        /// </summary>
        /// <param name="message">訊息</param>
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
