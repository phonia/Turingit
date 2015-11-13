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

        public static DataContext GetDataContext(string key)
        {
            if (!_dict.ContainsKey(key))
            {
                throw new Exception("not existence context in container");
            }
            return _dict[key];
        }

        public static void StoreContext(string key)
        {
            if (!_dict.ContainsKey(key))
            {
                _dict.Add(key, new DataContext());
            }
            else
            {
                throw new Exception("existence threadId in ContextContanier!");
            }
        }

        public static void Remove(string key)
        {
            if (_dict.ContainsKey(key))
            {
                _dict[key].Dispose();
                _dict.Remove(key);
            }
        }
    }
}
