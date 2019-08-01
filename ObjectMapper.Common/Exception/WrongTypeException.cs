using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectMapper.Common.Exception
{
    public class WrongTypeException : System.Exception
    {
        public WrongTypeException() : base() { }

        public WrongTypeException(string message) : base(message) { }
        
    }
}
