using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.Models;

namespace TuringL.Repository
{
    public class SysRecordUnitOfWorkRepository:Repository<SysRecord,System.Guid>,ISysRecordRepository
    {
        public SysRecordUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override SysRecord GetByKey(Guid Id)
        {
            return (GetAll() as IQueryable<SysRecord>).Where(it => it.Id == Id).FirstOrDefault();
        }
    }
}
