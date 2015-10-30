using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public interface IReadOnlyRepository<T,Tld>
    {
        T GetByKey(Tld Id);
        IQueryable<T> GetAll();
    }
}
