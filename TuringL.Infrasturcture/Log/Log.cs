using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Infrasturcture.Log
{
    interface ILog
    {
        void Write(string message);
    }
}
