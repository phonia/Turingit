using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public interface IRepositoryFactory
    {
        void Begin();
        void End();
        IUnitOfWorkRepository Get(Type type, IUnitOfWork unitOfWork);
        IUnitOfWork UnitOfWork { get; set; }
    }
}
