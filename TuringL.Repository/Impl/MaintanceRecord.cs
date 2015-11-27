using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.Models;

namespace TuringL.Repository
{
    public class MaintanceRecordUnitOfWorkRepository:Repository<Models.MaintanceRecord,System.Guid>
    {
        public MaintanceRecordUnitOfWorkRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public override Models.MaintanceRecord GetByKey(Guid Id)
        {
            return GetAll().Where(it => it.Id.Equals(Id)).FirstOrDefault();
        }
    }
}
