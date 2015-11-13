using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Infrasturcture.Log
{

    public class Log
    {
        private static object _lockObject = new object();

        public static void Write(string message)
        {
            lock (_lockObject)
            { }
        }
    }
}
