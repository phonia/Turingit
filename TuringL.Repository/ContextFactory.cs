using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TuringL.Models;

namespace TuringL.Repository
{
    public class ContextFactory
    {
        private static Dictionary<string, DataContext> _dict = new Dictionary<string, DataContext>();

        public static DataContext GetDataContext()
        {
            if (!_dict.ContainsKey(Thread.CurrentThread.ManagedThreadId+Thread.CurrentThread.Name))
            {
                throw new Exception("not existence context in container");
            }
            return _dict[Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name];
        }

        public static void StoreContext()
        {
            if (!_dict.ContainsKey(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name))
            {
                _dict.Add(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name, new DataContext());
            }
            else
            {
                throw new Exception("existence threadId in ContextContanier!");
            }
        }

        public static void Dispose()
        {
            if (_dict.ContainsKey(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name))
            {
                _dict[Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name].Dispose();
                _dict.Remove(Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.Name);
            }
        }
    }
}
