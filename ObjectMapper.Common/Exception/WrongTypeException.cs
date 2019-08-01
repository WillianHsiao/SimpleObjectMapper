using System;

namespace ObjectMapper.Common.Exception
{
    public class WrongTypeException : System.Exception
    {
        public WrongTypeException() : base() { }

        public WrongTypeException(string message) : base(message) { }

        /// <summary>
        /// 目標型別名稱
        /// </summary>
        public Type TargetPropertyType { get; set; }

        /// <summary>
        /// 來源型別名稱
        /// </summary>
        public Type SourcePropertyType { get; set; }

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

                return "型別無法轉換";
            }
        }
    }
}
