using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.YYException
{

    public class YYException : System.Exception
    {
        public YYException()
        { }

        public YYException(string msg)
                : base(msg)
        { }

        public YYException(string msg, Exception ex)
                : base(msg, ex)
        { }

        public YYException(SerializationInfo si, StreamingContext context)
                : base(si, context)
        {

        }
    }
}
