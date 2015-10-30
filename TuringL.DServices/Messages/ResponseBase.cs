using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.DServices.Messages
{
    public class ResponseBase
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
    }
}
