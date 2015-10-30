using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public interface IUnitOfWork
    {
        void RegisterAdd(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterDel(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterSave(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void Commit();
    }
}
