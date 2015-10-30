using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public interface IUnitOfWorkRepository
    {
        void PersistAdd(IAggregateRoot entity);
        void PersistDel(IAggregateRoot entity);
        void PersistSave(IAggregateRoot entity);
    }
}
