using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.Models;

namespace TuringL.Repository
{
    public class InstallUnitOfWorkRepository : Repository<InstallInfo, System.Guid>, IInstallInfoRepository
    {
        public InstallUnitOfWorkRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public override InstallInfo GetByKey(Guid Id)
        {
            return GetAll().Where(it => it.Id == Id).FirstOrDefault();
        }
    }
}
