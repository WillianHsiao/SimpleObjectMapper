namespace ObjectMapper.Common.Exception
{
    public class NameMissException : System.Exception
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public NameMissException() { }

        /// <summary>
        /// 建構子自訂訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public NameMissException(string message) : base(message) { }

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

                return "讀取單一值請指定欄位名稱";
            }
        }
    }
}
