using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class SysRecord:EntityBase,IAggregateRoot
    {
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISysRecordRepository : IRepository<SysRecord, System.Guid>
    { }
}
