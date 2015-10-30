using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public interface IRepository<T,Tld>:IReadOnlyRepository<T,Tld>
    {
        void Save(T entity);
        void Add(T entity);
        void Del(T entity);
    }
}
